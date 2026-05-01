using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Sothis.SothisCode.Cards.Vars;
using Sothis.SothisCode.Relics;

namespace Sothis.SothisCode.Cards.Uncommon;

public class SunRay : SothisCard
{
    public SunRay() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        (IEnumerable<DynamicVar>)
        [
            (DynamicVar) new DamageVar(38M, ValueProp.Move),
            (DynamicVar) new SoulHeatVar(2M)
        ];
    
    protected override async Task OnPlay(MegaCrit.Sts2.Core.GameActions.Multiplayer.PlayerChoiceContext choiceContext, CardPlay play)
    {
        SunRay card = this;
        SoulHeat soulHeat = GetSoulHeat();
        if (soulHeat.Amount >= 10)
        {
            ArgumentNullException.ThrowIfNull(play.Target);
            await DamageCmd.Attack(card.DynamicVars.Damage.BaseValue).FromCard((CardModel) card).Targeting(play.Target).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
            soulHeat.Amount = 0;
        }
        else
        {
            soulHeat.IncreaseSoulHeat(DynamicVars._vars["SoulHeat"].IntValue);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(10m);
        DynamicVars._vars["SoulHeat"].UpgradeValueBy(2m);
    }
}