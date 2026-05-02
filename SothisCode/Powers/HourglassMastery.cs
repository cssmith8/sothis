using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace Sothis.SothisCode.Powers;

public class HourglassMastery : SothisPower
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

    public override async Task AfterCardPlayedLate(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (!Active) return;
        this.Flash();
        CardModel clone = cardPlay.Card.CreateClone();
        CardCmd.ApplyKeyword(clone, CardKeyword.Exhaust);
        CardCmd.ApplyKeyword(clone, CardKeyword.Ethereal);
        clone.EnergyCost.SetUntilPlayed(0);
        await CardPileCmd.AddGeneratedCardToCombat(clone, PileType.Hand, true);
        await PowerCmd.Decrement(this);
    }
}