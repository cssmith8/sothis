using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Sothis.SothisCode.Powers;

public class SandsOfTime : SothisPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    private bool _active = false;

    private bool Active
    {
        get => this._active;
        set
        {
            this.AssertMutable();
            this._active = value;
        }
    }

    public override async Task BeforeCardPlayed(CardPlay cardPlay)
    {
        Active = true;
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (!Active) return;
        if (!Owner.IsPlayer || Owner.Player is null) return;
        foreach (PowerModel power in this.Owner.Powers)
        {
            if (power is HourglassMastery && power.Amount > 0) return;
        }
        
        int randomNumber = Owner.Player.RunState.Rng.CombatTargets.NextInt(0, 100);
        if (randomNumber >= Amount) return;
        
        this.Flash();
        CardModel clone = cardPlay.Card.CreateClone();
        CardCmd.ApplyKeyword(clone, CardKeyword.Exhaust);
        CardCmd.ApplyKeyword(clone, CardKeyword.Ethereal);
        clone.EnergyCost.SetUntilPlayed(0);
        await CardPileCmd.AddGeneratedCardToCombat(clone, PileType.Hand, true);

        int offset = 5;
        if (Amount < offset) offset = Amount;
        await PowerCmd.ModifyAmount(this, 0M - offset, null, null);
    }

    //this only runs when you get it for the first time
    public override async Task AfterApplied(Creature? applier, CardModel? cardSource)
    {
        MegaCrit.Sts2.Core.Logging.Log.Info("Applied SandsOfTime");
        if (Amount > 50)
        {
            await PowerCmd.ModifyAmount(this, 50M - Amount, null, null);
        }
    }
}