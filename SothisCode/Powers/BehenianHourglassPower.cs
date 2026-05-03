using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Sothis.SothisCode.Powers;

public class BehenianHourglassPower : SothisPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    // public override string CustomPackedIconPath => "behenian_hourglass.png".PowerImagePath();
    // public override string CustomBigIconPath => "behenian_hourglass.png".BigPowerImagePath();

    public override async Task AfterPlayerTurnStartEarly(PlayerChoiceContext choiceContext, Player player)
    {
        await PowerCmd.Apply<HourglassMastery>(this.Owner, this.Amount, null, null);
    }
}