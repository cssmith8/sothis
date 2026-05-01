using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Sothis.SothisCode.Cards.Vars;

public class AfflictionVar : DynamicVar
{
    public const string Key = "Affliction";

    public AfflictionVar(decimal baseValue) : base(Key, baseValue)
    {
        this.WithTooltip();
    }
}