using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using Sothis.SothisCode.Character;
using Sothis.SothisCode.Extensions;
using Godot;

namespace Sothis.SothisCode.Relics;

[Pool(typeof(SothisRelicPool))]
public abstract class SothisRelic : CustomRelicModel
{
    public override string PackedIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".RelicImagePath();

    protected override string PackedIconOutlinePath =>
        $"{Id.Entry.RemovePrefix().ToLowerInvariant()}_outline.png".RelicImagePath();

    protected override string BigIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigRelicImagePath();
}