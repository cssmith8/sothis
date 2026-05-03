using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using Sothis.SothisCode.Cards.Vars;
using Sothis.SothisCode.Powers;

namespace Sothis.SothisCode.Cards.Rare;

public class BehenianHourglass : SothisCard
{
    public BehenianHourglass() : base(3, CardType.Power, CardRarity.Rare, TargetType.Self)
    {
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        (IEnumerable<DynamicVar>)
        [
            (DynamicVar) new HourglassMasteryVar(1)
        ];
    
    protected override async Task OnPlay(MegaCrit.Sts2.Core.GameActions.Multiplayer.PlayerChoiceContext choiceContext, CardPlay play)
    {
        BehenianHourglass card = this;
        await PowerCmd.Apply<BehenianHourglassPower>(card.Owner.Creature, this.DynamicVars._vars["HourglassMastery"].IntValue, card.Owner.Creature, (CardModel) card);
    }

    protected override void OnUpgrade() => this.EnergyCost.UpgradeBy(-1);
}