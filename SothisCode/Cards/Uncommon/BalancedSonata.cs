using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using Sothis.SothisCode.Cards.Vars;
using Sothis.SothisCode.Powers;

namespace Sothis.SothisCode.Cards.Uncommon;

public class BalancedSonata : SothisCard
{
    public BalancedSonata() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AllEnemies)
    {
    }

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        (IEnumerable<DynamicVar>)
        [
            (DynamicVar) new ReliefVar(2M),
            (DynamicVar) new AfflictionVar(2M),
        ];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords =>
        (IEnumerable<CardKeyword>) 
        [
            CardKeyword.Exhaust
        ];
    
    protected override async Task OnPlay(MegaCrit.Sts2.Core.GameActions.Multiplayer.PlayerChoiceContext choiceContext, CardPlay play)
    {
        BalancedSonata cardSource = this;
        ArgumentNullException.ThrowIfNull(cardSource.CombatState);
        await CreatureCmd.TriggerAnim(cardSource.Owner.Creature, "Cast", cardSource.Owner.Character.CastAnimDelay);
        foreach (Creature hittableEnemy in (IEnumerable<Creature>) cardSource.CombatState.HittableEnemies)
        {
            await PowerCmd.Apply<ReliefEnemy>(hittableEnemy, cardSource.DynamicVars["Relief"].BaseValue, cardSource.Owner.Creature, (CardModel) cardSource);
            await PowerCmd.Apply<AfflictionEnemy>(hittableEnemy, cardSource.DynamicVars["Affliction"].BaseValue, cardSource.Owner.Creature, (CardModel) cardSource);
        }
        await PowerCmd.Apply<ReliefAlly>(cardSource.Owner.Creature, cardSource.DynamicVars["Relief"].BaseValue, cardSource.Owner.Creature, (CardModel) cardSource);
        await PowerCmd.Apply<AfflictionAlly>(cardSource.Owner.Creature, cardSource.DynamicVars["Affliction"].BaseValue, cardSource.Owner.Creature, (CardModel) cardSource);
    }
    
    protected override void OnUpgrade() => this.DynamicVars._vars["Relief"].UpgradeValueBy(2M);
}