using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Sothis.SothisCode.Extensions;

namespace Sothis.SothisCode.Powers;

public class FlammableChakramPower : SothisPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override string CustomPackedIconPath => "flammable_chakram.png".PowerImagePath();
    public override string CustomBigIconPath => "flammable_chakram.png".BigPowerImagePath();

    public override async Task AfterDamageGiven(
        PlayerChoiceContext choiceContext,
        Creature? dealer,
        DamageResult result,
        ValueProp props,
        Creature target,
        CardModel? cardSource)
    {
        FlammableChakramPower power = this;
        if (dealer?.Player == null
            || dealer.Player.Creature != power.Owner
            || !CombatManager.Instance.IsInProgress
            || result.TotalDamage <= 0
            )
        {
            return;
        }
        if (target.Side == CombatSide.Enemy)
        {
            await PowerCmd.Apply<ReliefEnemy>(target, power.Amount, power.Owner, null);
        }
        else
        {
            await PowerCmd.Apply<ReliefAlly>(target, power.Amount, power.Owner, null);
        }
    }

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side != CombatSide.Enemy)
            return;
        FlammableChakramPower flammableChakramPower = this;
        await PowerCmd.Decrement((PowerModel) flammableChakramPower);
    }
}