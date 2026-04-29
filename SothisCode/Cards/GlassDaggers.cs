using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Sothis.SothisCode.Cards;

public class GlassDaggers : SothisCard
{
    public GlassDaggers() : base(-1, CardType.Attack, CardRarity.Common, Sothis.SothisCode.CustomTargetType.Anyone)
    {
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        (IEnumerable<DynamicVar>)
        [
            (DynamicVar) new DamageVar(1000000000M, ValueProp.Move),
            (DynamicVar) new GoldVar(1000000000)
        ];
    
    protected override async Task OnPlay(MegaCrit.Sts2.Core.GameActions.Multiplayer.PlayerChoiceContext choiceContext, CardPlay play)
    {
        GlassDaggers card = this;
        await CommonActions.CardAttack(this, play.Target).Execute(choiceContext);
        await PlayerCmd.GainGold((Decimal) (0 - card.DynamicVars["Gold"].IntValue), card.Owner);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3m);
    }
}