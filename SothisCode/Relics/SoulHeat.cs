using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;
using Sothis.SothisCode.Powers;

namespace Sothis.SothisCode.Relics;

public class SoulHeat : SothisRelic
{
    public override RelicRarity Rarity => RelicRarity.Starter;

    private int _amount;
    
    public override bool ShowCounter => CombatManager.Instance.IsInProgress;
    
    public int Amount
    {
        get => this._amount;
        private set
        {
            this.AssertMutable();
            this._amount = value;
            this.UpdateDisplay();
        }
    }
    
    public override int DisplayAmount => Amount;

    public int SetSoulHeat(int amountToSet)
    {
        Amount = (amountToSet > 0) ? amountToSet : 0;
        return amountToSet;
    }

    public int IncreaseSoulHeat(int amountToIncrease)
    {
        int currentHeat = Amount;
        currentHeat += amountToIncrease;
        if (currentHeat <= 0) currentHeat = 0;
        Amount = currentHeat;
        return currentHeat;
    }
    
    public int DecreaseSoulHeat(int amountToDecrease)
    {
        return IncreaseSoulHeat(0 - amountToDecrease);
    }

    private void ResetSoulHeat()
    {
        int amountToReset = 0;
        foreach (PowerModel power in this.Owner.Creature.Powers)
        {
            if (power is not ConcentrationPower concentrationPower) continue;
            amountToReset += concentrationPower.Amount;
        }
        Amount = amountToReset;
    }
    
    private void UpdateDisplay()
    {
        this.InvokeDisplayAmountChanged();
    }
    
    public override Task BeforeCombatStart()
    {
        this.ResetSoulHeat();
        return Task.CompletedTask;
    }
    
    public override Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side == CombatSide.Player) return Task.CompletedTask;
        this.ResetSoulHeat();
        return Task.CompletedTask;
    }
    
    public override Task AfterDamageGiven(
        PlayerChoiceContext choiceContext,
        Creature? dealer,
        DamageResult result,
        ValueProp props,
        Creature target,
        CardModel? cardSource)
    {
        SoulHeat soulHeat = this;
        if (dealer == null
            || dealer.Player != soulHeat.Owner
            || !CombatManager.Instance.IsInProgress
            || result.TotalDamage <= 0
            || dealer == target)
        {
            return Task.CompletedTask;
        }
        Amount++;
        return Task.CompletedTask;
    }
    
    public override Task AfterPowerAmountChanged(
        PowerModel power,
        Decimal amount,
        Creature? applier,
        CardModel? cardSource)
    {
        SoulHeat soulHeat = this;
        if (power is not (ReliefAlly or ReliefEnemy) || amount <= 0) return Task.CompletedTask;
        Amount++;
        return Task.CompletedTask;
    }
}