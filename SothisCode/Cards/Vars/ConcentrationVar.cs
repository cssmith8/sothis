using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Sothis.SothisCode.Cards.Vars;

public class ConcentrationVar : DynamicVar
{
    public const string Key = "Concentration";

    public ConcentrationVar(decimal baseValue) : base(Key, baseValue)
    {
        this.WithTooltip();
    }
}