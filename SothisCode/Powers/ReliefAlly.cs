using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Sothis.SothisCode.Extensions;

namespace Sothis.SothisCode.Powers;

public class ReliefAlly : SothisPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override string CustomPackedIconPath => "relief.png".PowerImagePath();
    public override string CustomBigIconPath => "relief.png".BigPowerImagePath();
    
    public override async Task BeforeTurnEndEarly(PlayerChoiceContext choiceContext, CombatSide side)
    {
        ReliefAlly relief = this;
        if (side != relief.Owner.Side)
            return;
        await CreatureCmd.GainBlock(relief.Owner, relief.Amount, ValueProp.Unpowered, null);
        relief.Flash();
    }

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side != CombatSide.Enemy)
            return;
        ReliefAlly relief = this;
        await PowerCmd.Decrement((PowerModel) relief);
    }
}