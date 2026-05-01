using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;
using Sothis.SothisCode.Cards.Vars;
using Sothis.SothisCode.Powers;
using Sothis.SothisCode.Relics;

namespace Sothis.SothisCode.Cards.Uncommon;

public class Conflagration : SothisCard
{
    public Conflagration() : base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies)
    {
    }
    
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        (IEnumerable<DynamicVar>)
        [
            (DynamicVar) new DynamicVar("Factor", 5M),
            (DynamicVar) new AfflictionVar(1M)
        ];
    
    protected override async Task OnPlay(MegaCrit.Sts2.Core.GameActions.Multiplayer.PlayerChoiceContext choiceContext, CardPlay play)
    {
        Conflagration card = this;
        ArgumentNullException.ThrowIfNull(card.CombatState);
        ArgumentNullException.ThrowIfNull(card.Owner.Creature.CombatState);
        IReadOnlyList<Creature> hittableEnemies = card.Owner.Creature.CombatState.HittableEnemies;
        Creature? target = card.Owner.RunState.Rng.CombatTargets.NextItem<Creature>((IEnumerable<Creature>) hittableEnemies);
        if (target == null) return;
        AfflictionEnemy? afflictionEnemy = await PowerCmd.Apply<AfflictionEnemy>(target, card.DynamicVars._vars["Affliction"].BaseValue, card.Owner.Creature, (CardModel) card);
        if (afflictionEnemy == null) return;
        await DamageCmd.Attack(card.DynamicVars._vars["Factor"].BaseValue * afflictionEnemy._amount).FromCard((CardModel) card).TargetingAllOpponents(card.CombatState).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars._vars["Factor"].UpgradeValueBy(2m);
    }
}