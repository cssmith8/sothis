using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Sothis.SothisCode.Cards.Vars;

public class SoulHeatVar : DynamicVar
{
    public const string Key = "SoulHeat";

    public SoulHeatVar(decimal baseValue) : base(Key, baseValue)
    {
        this.WithTooltip();
    }
}