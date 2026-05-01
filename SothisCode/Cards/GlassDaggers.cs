using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Sothis.SothisCode.Cards;

public class GlassDaggers : SothisCard
{
    public GlassDaggers() : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
    {
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        (IEnumerable<DynamicVar>)
        [
            (DynamicVar) new DamageVar(1M, ValueProp.Move),
            (DynamicVar) new RepeatVar(5)
        ];
    
    protected override async Task OnPlay(MegaCrit.Sts2.Core.GameActions.Multiplayer.PlayerChoiceContext choiceContext, CardPlay play)
    {
        GlassDaggers card = this;
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        await DamageCmd.Attack(card.DynamicVars.Damage.BaseValue).WithHitCount(card.DynamicVars.Repeat.IntValue).FromCard((CardModel) card).Targeting(play.Target).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Repeat.UpgradeValueBy(3m);
    }
}