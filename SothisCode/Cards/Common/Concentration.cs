using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using Sothis.SothisCode.Cards.Vars;
using Sothis.SothisCode.Powers;

namespace Sothis.SothisCode.Cards.Common;

public class Concentration : SothisCard
{
    public Concentration() : base(1, CardType.Power, CardRarity.Common, TargetType.Self)
    {
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        (IEnumerable<DynamicVar>)
        [
            (DynamicVar) new DynamicVar("Concentration", 2M),
            (DynamicVar) new SoulHeatVar(0M)
        ];
    
    protected override async Task OnPlay(MegaCrit.Sts2.Core.GameActions.Multiplayer.PlayerChoiceContext choiceContext, CardPlay play)
    {
        Concentration card = this;
        await PowerCmd.Apply<ConcentrationPower>(card.Owner.Creature, card.DynamicVars["Concentration"].BaseValue, card.Owner.Creature, (CardModel) card);
    }

    protected override void OnUpgrade() => this.DynamicVars._vars["Concentration"].UpgradeValueBy(1M);
}