using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Sothis.SothisCode.Cards;
using Sothis.SothisCode.Powers;

namespace Sothis.SothisCode.Cards;

public class Purification : SothisCard
{
    public Purification() : base(1, CardType.Skill, CardRarity.Basic, Sothis.SothisCode.CustomTargetType.Anyone)
    {
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        (IEnumerable<DynamicVar>)
        [
            (DynamicVar) new PurifyVar(6M)
        ];
    
    protected override async Task OnPlay(MegaCrit.Sts2.Core.GameActions.Multiplayer.PlayerChoiceContext choiceContext, CardPlay play)
    {
        Purification card = this;
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        if (play.Target.Side == CombatSide.Enemy)
        {
            await DamageCmd.Attack(card.DynamicVars._vars["Purify"].BaseValue).FromCard((CardModel) card).Targeting(play.Target).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
        }
        else
        {
            await CreatureCmd.GainBlock(card.Owner.Creature, card.DynamicVars._vars["Purify"].BaseValue, new ValueProp(), play);
        }
    }
    
    protected override void OnUpgrade() => this.DynamicVars._vars["Purify"].UpgradeValueBy(3M);
}