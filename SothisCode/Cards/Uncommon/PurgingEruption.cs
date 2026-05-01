using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Sothis.SothisCode.Cards.Vars;
using Sothis.SothisCode.Powers;

namespace Sothis.SothisCode.Cards.Uncommon;

public class PurgingEruption : SothisCard
{
    public PurgingEruption() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        (IEnumerable<DynamicVar>)
        [
            (DynamicVar) new DamageVar(3M, ValueProp.Move),
            (DynamicVar) new AfflictionVar(1M)
        ];
    
    protected override async Task OnPlay(MegaCrit.Sts2.Core.GameActions.Multiplayer.PlayerChoiceContext choiceContext, CardPlay play)
    {
        PurgingEruption card = this;
        ArgumentNullException.ThrowIfNull(play.Target);
        AfflictionEnemy? afflictionEnemy = await PowerCmd.Apply<AfflictionEnemy>(play.Target, card.DynamicVars._vars["Affliction"].BaseValue, card.Owner.Creature, (CardModel) card);
        if (afflictionEnemy is not { Amount: > 0 }) return;
        await DamageCmd.Attack(card.DynamicVars.Damage.BaseValue).WithHitCount(afflictionEnemy.Amount).FromCard((CardModel) card).Targeting(play.Target).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2m);
    }
}