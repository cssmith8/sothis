using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using Sothis.SothisCode.Extensions;

namespace Sothis.SothisCode.Powers;

public class InnerWarmthPower : SothisPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override string CustomPackedIconPath => "inner_warmth.png".PowerImagePath();
    public override string CustomBigIconPath => "inner_warmth.png".BigPowerImagePath();
    
}