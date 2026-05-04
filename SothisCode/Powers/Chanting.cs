using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Sothis.SothisCode.Cards;

namespace Sothis.SothisCode.Powers;

public class Chanting : SothisPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override Task AfterDamageGiven(
        PlayerChoiceContext choiceContext,
        Creature? dealer,
        DamageResult result,
        ValueProp props,
        Creature target,
        CardModel? cardSource)
    {
        Chanting chanting = this;
        if (dealer == null
            || dealer != chanting.Owner
            || !CombatManager.Instance.IsInProgress
            || result.TotalDamage <= 0 
            || cardSource == null
            || cardSource is SothisCard { isChantingCard: true }
            )
        {
            return Task.CompletedTask;
        }
        PowerCmd.Remove(chanting);
        return Task.CompletedTask;
    }
}