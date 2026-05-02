using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Sothis.SothisCode.Cards.Vars;

public class DistortionVar : DynamicVar
{
    public const string Key = "Distortion";

    public DistortionVar(decimal baseValue) : base(Key, baseValue)
    {
        this.WithTooltip();
    }
}