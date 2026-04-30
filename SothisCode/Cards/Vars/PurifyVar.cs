using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Sothis.SothisCode.Cards;

public class PurifyVar : DynamicVar
{
    public const string Key = "Purify";

    public PurifyVar(decimal baseValue) : base(Key, baseValue)
    {
        this.WithTooltip();
    }
}