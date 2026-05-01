using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Sothis.SothisCode.Cards.Vars;
using Sothis.SothisCode.Powers;
using Sothis.SothisCode.Relics;

namespace Sothis.SothisCode.Cards.Uncommon;

public class GhastlyFire : SothisCard
{
    public GhastlyFire() : base(1, CardType.Attack, CardRarity.Uncommon, Sothis.SothisCode.CustomTargetType.Anyone)
    {
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        (IEnumerable<DynamicVar>)
        [
            (DynamicVar) new DynamicVar("SoulHeatGain", 3),
            (DynamicVar) new DynamicVar("SoulHeatLoss", 5),
            (DynamicVar) new SoulHeatVar(5),
            (DynamicVar) new ReliefVar(7),
            (DynamicVar) new AfflictionVar(4)
        ];
    
    protected override async Task OnPlay(MegaCrit.Sts2.Core.GameActions.Multiplayer.PlayerChoiceContext choiceContext, CardPlay play)
    {
        GhastlyFire card = this;
        SoulHeat soulHeat = GetSoulHeat();
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        await CreatureCmd.TriggerAnim(card.Owner.Creature, "Cast", card.Owner.Character.CastAnimDelay);
        if (soulHeat.Amount >= card.DynamicVars._vars["SoulHeat"].BaseValue)
        {
            if (play.Target.Side == CombatSide.Enemy)
            {
                await PowerCmd.Apply<ReliefEnemy>(play.Target, card.DynamicVars._vars["Relief"].BaseValue, card.Owner.Creature, (CardModel) card);
            }
            else
            {
                await PowerCmd.Apply<ReliefAlly>(play.Target, card.DynamicVars._vars["Relief"].BaseValue, card.Owner.Creature, (CardModel) card);
            }
            soulHeat.Amount -= card.DynamicVars._vars["SoulHeatLoss"].IntValue;
        }
        else
        {
            if (play.Target.Side == CombatSide.Enemy)
            {
                await PowerCmd.Apply<AfflictionEnemy>(play.Target, card.DynamicVars._vars["Affliction"].BaseValue, card.Owner.Creature, (CardModel) card);
            }
            else
            {
                await PowerCmd.Apply<AfflictionAlly>(play.Target, card.DynamicVars._vars["Affliction"].BaseValue, card.Owner.Creature, (CardModel) card);
            }
            soulHeat.Amount += card.DynamicVars._vars["SoulHeatGain"].IntValue;
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars._vars["Relief"].UpgradeValueBy(2);
        DynamicVars._vars["SoulHeatGain"].UpgradeValueBy(2);
        DynamicVars._vars["SoulHeatLoss"].UpgradeValueBy(-2);
    }
}