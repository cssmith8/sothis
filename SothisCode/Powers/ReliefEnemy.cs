using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Sothis.SothisCode.Powers;

public class ReliefEnemy : SothisPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    /*
    public override async Task BeforeTurnEndEarly(PlayerChoiceContext choiceContext, CombatSide side)
    {
        ReliefEnemy relief = this;
        if (side != relief.Owner.Side)
            return;
        relief.Flash();
        await CreatureCmd.GainBlock(relief.Owner, relief.Amount, ValueProp.Unpowered, null);
    }
    */

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side != CombatSide.Enemy)
            return;
        ReliefEnemy relief = this;
        await CreatureCmd.Damage(choiceContext, relief.Owner, relief.Amount, ValueProp.Unpowered, relief.Owner, null);
        VfxCmd.PlayOnCreatureCenter(relief.Owner, "vfx/vfx_attack_blunt");
        await PowerCmd.Decrement((PowerModel) relief);
    }
}