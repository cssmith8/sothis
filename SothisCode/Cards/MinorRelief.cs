using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Sothis.SothisCode.Cards.Vars;
using Sothis.SothisCode.Powers;

namespace Sothis.SothisCode.Cards;

public class MinorRelief : SothisCard
{
    public MinorRelief() : base(1, CardType.Skill, CardRarity.Basic, Sothis.SothisCode.CustomTargetType.Anyone)
    {
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        (IEnumerable<DynamicVar>)
        [
            (DynamicVar) new ReliefVar(3M)
        ];
    
    protected override async Task OnPlay(MegaCrit.Sts2.Core.GameActions.Multiplayer.PlayerChoiceContext choiceContext, CardPlay play)
    {
        MinorRelief card = this;
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        if (play.Target.Side == CombatSide.Enemy)
        {
            await PowerCmd.Apply<ReliefEnemy>(play.Target, card.DynamicVars._vars["Relief"].BaseValue, card.Owner.Creature, (CardModel) card);
        }
        else
        {
            await PowerCmd.Apply<ReliefAlly>(play.Target, card.DynamicVars._vars["Relief"].BaseValue, card.Owner.Creature, (CardModel) card);
        }
    }
    
    protected override void OnUpgrade() => this.DynamicVars._vars["Relief"].UpgradeValueBy(2M);
}