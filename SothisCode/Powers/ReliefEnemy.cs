using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Sothis.SothisCode.Extensions;

namespace Sothis.SothisCode.Powers;

public class ReliefEnemy : SothisPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override string CustomPackedIconPath => "relief.png".PowerImagePath();
    public override string CustomBigIconPath => "relief.png".BigPowerImagePath();

    public override async Task AfterTurnEndLate(PlayerChoiceContext choiceContext, CombatSide side)
    {
        ReliefEnemy relief = this;
        if (side != relief.Owner.Side)
            return;
        await CreatureCmd.Damage(choiceContext, relief.Owner, relief.Amount, ValueProp.Unpowered, relief.Owner, null);
        relief.Flash();
        VfxCmd.PlayOnCreatureCenter(relief.Owner, "vfx/vfx_attack_blunt");
        await PowerCmd.Decrement((PowerModel) relief);
    }
}