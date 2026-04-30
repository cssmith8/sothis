using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Sothis.SothisCode.Cards;

public class CorruptVar : DynamicVar
{
    public const string Key = "Corrupt";

    public CorruptVar(decimal baseValue) : base(Key, baseValue)
    {
        this.WithTooltip();
    }
}