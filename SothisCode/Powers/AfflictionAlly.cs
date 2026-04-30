using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Sothis.SothisCode.Powers;

public class AfflictionAlly : SothisPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override async Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        AfflictionAlly affliction = this;
        if (side != affliction.Owner.Side)
            return;
        await CreatureCmd.Damage(choiceContext, affliction.Owner, affliction.Amount, ValueProp.Unpowered, affliction.Owner, null);
        affliction.Flash();
        VfxCmd.PlayOnCreatureCenter(affliction.Owner, "vfx/vfx_attack_blunt");
    }
}