using MegaCrit.Sts2.Core.Entities.Powers;
using Sothis.SothisCode.Extensions;

namespace Sothis.SothisCode.Powers;

public class Distortion : SothisPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
}