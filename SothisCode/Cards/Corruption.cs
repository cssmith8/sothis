using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Sothis.SothisCode.Cards.Vars;

namespace Sothis.SothisCode.Cards;

public class Corruption : SothisCard
{
    public Corruption() : base(0, CardType.Skill, CardRarity.Basic, Sothis.SothisCode.CustomTargetType.Anyone)
    {
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        (IEnumerable<DynamicVar>)
        [
            (DynamicVar) new CorruptVar(4M),
            (DynamicVar) new CardsVar(1)
        ];
    
    protected override async Task OnPlay(MegaCrit.Sts2.Core.GameActions.Multiplayer.PlayerChoiceContext choiceContext, CardPlay play)
    {
        Corruption card = this;
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        await CreatureCmd.TriggerAnim(card.Owner.Creature, "Cast", card.Owner.Character.CastAnimDelay);
        if (play.Target.Side == CombatSide.Enemy)
        {
            await CreatureCmd.Heal(play.Target, card.DynamicVars._vars["Corrupt"]._baseValue);
        }
        else
        {
            await DamageCmd.Attack(card.DynamicVars._vars["Corrupt"]._baseValue).FromCard((CardModel) card).Targeting(play.Target).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
        }
        await CardPileCmd.Draw(choiceContext, card.DynamicVars.Cards.BaseValue, card.Owner);
    }
    
    public override bool HasTurnEndInHandEffect => true;

    public override async Task OnTurnEndInHand(PlayerChoiceContext choiceContext)
    {
        Corruption cardSource = this;
        await CreatureCmd.Damage(choiceContext, cardSource.Owner.Creature, cardSource.DynamicVars._vars["Corrupt"]._baseValue, new ValueProp(), (CardModel) cardSource);
    }
    
    protected override void OnUpgrade() => this.DynamicVars._vars["Corrupt"].UpgradeValueBy(-2M);
}