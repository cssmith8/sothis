using BaseLib.Abstracts;
using BaseLib.Extensions;
using Sothis.SothisCode.Extensions;
using Godot;

namespace Sothis.SothisCode.Powers;

public abstract class SothisPower : CustomPowerModel
{
    //Loads from Sothis/images/powers/your_power.png
    public override string CustomPackedIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PowerImagePath();
    public override string CustomBigIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigPowerImagePath();
}