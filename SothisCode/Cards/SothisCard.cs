using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using Sothis.SothisCode.Character;
using Sothis.SothisCode.Extensions;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;
using Sothis.SothisCode.Powers;
using Sothis.SothisCode.Relics;

namespace Sothis.SothisCode.Cards;

[Pool(typeof(SothisCardPool))]
public abstract class SothisCard(int cost, CardType type, CardRarity rarity, TargetType target) :
    CustomCardModel(cost, type, rarity, target)
{
    protected SoulHeat GetSoulHeat()
    {
        foreach (RelicModel relic in this.Owner.Relics)
        {
            if (relic is not SoulHeat soulHeat) continue;
            return soulHeat;
        }
        return new SoulHeat();
    }

    protected async Task<int> TryUseDistortion(int heat)
    {
        foreach (PowerModel power in this.Owner.Creature.Powers)
        {
            if (power is not Distortion distortion) continue;
            await PowerCmd.Decrement((PowerModel) distortion);
            return 10;
        }
        return heat;
    }
    
    //Image size:
    //Normal art: 1000x760
    //Full art: 606x852
    public override string CustomPortraitPath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath();
            return ResourceLoader.Exists(path) ? path : "card.png".BigCardImagePath();
        }
    }
    
    public override string PortraitPath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
            return ResourceLoader.Exists(path) ? path : "card.png".CardImagePath();
        }
    }
    
    public override string BetaPortraitPath
    {
        get
        {
            var path = $"beta/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
            return ResourceLoader.Exists(path) ? path : "card.png".CardImagePath();
        }
    }
}