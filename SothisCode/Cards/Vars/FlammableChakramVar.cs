using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Sothis.SothisCode.Cards.Vars;

public class FlammableChakramVar : DynamicVar
{
    public const string Key = "FlammableChakram";

    public FlammableChakramVar(decimal baseValue) : base(Key, baseValue)
    {
        this.WithTooltip();
    }
}