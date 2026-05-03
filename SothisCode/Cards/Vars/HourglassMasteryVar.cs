using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Sothis.SothisCode.Cards.Vars;

public class HourglassMasteryVar : DynamicVar
{
    public const string Key = "HourglassMastery";

    public HourglassMasteryVar(decimal baseValue) : base(Key, baseValue)
    {
        this.WithTooltip();
    }
}