using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using Sothis.SothisCode.Cards.Vars;
using Sothis.SothisCode.Powers;

namespace Sothis.SothisCode.Cards.Common;

public class DistortionHieroglyph : SothisCard
{
    public DistortionHieroglyph() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        (IEnumerable<DynamicVar>)
        [
            (DynamicVar) new DynamicVar("Distortion", 1M),
            (DynamicVar) new SoulHeatVar(0M)
        ];
    
    protected override async Task OnPlay(MegaCrit.Sts2.Core.GameActions.Multiplayer.PlayerChoiceContext choiceContext, CardPlay play)
    {
        DistortionHieroglyph card = this;
        await PowerCmd.Apply<Distortion>(card.Owner.Creature, card.DynamicVars["Distortion"].BaseValue, card.Owner.Creature, (CardModel) card);
    }

    protected override void OnUpgrade() => this.EnergyCost.UpgradeBy(-1);
}