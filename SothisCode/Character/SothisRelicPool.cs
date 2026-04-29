using BaseLib.Abstracts;
using Sothis.SothisCode.Extensions;
using Godot;

namespace Sothis.SothisCode.Character;

public class SothisRelicPool : CustomRelicPoolModel
{
    public override Color LabOutlineColor => Sothis.Color;

    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}