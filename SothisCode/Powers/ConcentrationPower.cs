using MegaCrit.Sts2.Core.Entities.Powers;
using Sothis.SothisCode.Extensions;

namespace Sothis.SothisCode.Powers;

public class ConcentrationPower : SothisPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override string CustomPackedIconPath => "concentration.png".PowerImagePath();
    public override string CustomBigIconPath => "concentration.png".BigPowerImagePath();
    
    
}