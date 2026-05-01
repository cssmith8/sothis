using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using Sothis.SothisCode.Cards.Vars;
using Sothis.SothisCode.Powers;

namespace Sothis.SothisCode.Cards.Rare;

public class FlammableChakram : SothisCard
{
    public FlammableChakram() : base(2, CardType.Power, CardRarity.Rare, TargetType.Self)
    {
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        (IEnumerable<DynamicVar>)
        [
            (DynamicVar) new FlammableChakramVar(5M),
            (DynamicVar) new ReliefVar(0M)
        ];
    
    protected override async Task OnPlay(MegaCrit.Sts2.Core.GameActions.Multiplayer.PlayerChoiceContext choiceContext, CardPlay play)
    {
        FlammableChakram card = this;
        await PowerCmd.Apply<FlammableChakramPower>(card.Owner.Creature, card.DynamicVars["FlammableChakram"].BaseValue, card.Owner.Creature, (CardModel) card);
    }

    protected override void OnUpgrade() => this.EnergyCost.UpgradeBy(-1);
}