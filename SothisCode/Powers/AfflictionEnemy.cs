using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Sothis.SothisCode.Extensions;

namespace Sothis.SothisCode.Powers;

public class AfflictionEnemy : SothisPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override string CustomPackedIconPath => "affliction.png".PowerImagePath();
    public override string CustomBigIconPath => "affliction.png".BigPowerImagePath();

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        AfflictionEnemy affliction = this;
        if (side != affliction.Owner.Side)
            return;
        await CreatureCmd.Heal(affliction.Owner, affliction.Amount);
        affliction.Flash();
    }
}