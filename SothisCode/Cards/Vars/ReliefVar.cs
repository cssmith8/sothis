using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Sothis.SothisCode.Cards.Vars;

public class ReliefVar : DynamicVar
{
    public const string Key = "Relief";

    public ReliefVar(decimal baseValue) : base(Key, baseValue)
    {
        this.WithTooltip();
    }
}