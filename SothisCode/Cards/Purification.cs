using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Sothis.SothisCode.Cards;

namespace Sothis.SothisCode.Cards;

public class Purification : SothisCard
{
    public Purification() : base(1, CardType.Attack, CardRarity.Common, Sothis.SothisCode.CustomTargetType.Anyone)
    {
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        (IEnumerable<DynamicVar>)
        [
            (DynamicVar) new DamageVar(6M, ValueProp.Move),
            (DynamicVar) new BlockVar(6M, ValueProp.Move)
        ];
    
    protected override async Task OnPlay(MegaCrit.Sts2.Core.GameActions.Multiplayer.PlayerChoiceContext choiceContext, CardPlay play)
    {
        Purification card = this;
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        if (play.Target == card.Owner.Creature)
        {
            await CreatureCmd.GainBlock(card.Owner.Creature, card.DynamicVars.Block, play);
        }
        else
        {
            await DamageCmd.Attack(card.DynamicVars.Damage.BaseValue).FromCard((CardModel) card).Targeting(play.Target).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
        }
    }
}