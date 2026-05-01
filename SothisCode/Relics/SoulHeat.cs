using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
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
        set
        {
            this.AssertMutable();
            this._amount = value;
            this.UpdateDisplay();
        }
    }
    
    public override int DisplayAmount => Amount;
    
    private void UpdateDisplay()
    {
        this.InvokeDisplayAmountChanged();
    }
    
    public override Task BeforeCombatStart()
    {
        Amount = 0;
        // this.Status = RelicStatus.Normal;
        return Task.CompletedTask;
    }
    
    public override Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        Amount = 0;
        // this.Status = RelicStatus.Normal;
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