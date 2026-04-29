using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using Sothis.SothisCode.Character;
using Sothis.SothisCode.Extensions;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;

namespace Sothis.SothisCode.Cards;

[Pool(typeof(SothisCardPool))]
public abstract class SothisCard(int cost, CardType type, CardRarity rarity, TargetType target) :
    CustomCardModel(cost, type, rarity, target)
{
    //Image size:
    //Normal art: 1000x760
    //Full art: 606x852
    public override string CustomPortraitPath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath();
            MegaCrit.Sts2.Core.Logging.Log.Info($"Sothis searching for {Id.Entry.RemovePrefix().ToLowerInvariant()}.png");
            return ResourceLoader.Exists(path) ? path : "card.png".BigCardImagePath();
        }
    }
    
    public override string PortraitPath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
            MegaCrit.Sts2.Core.Logging.Log.Info($"Sothis searching for {Id.Entry.RemovePrefix().ToLowerInvariant()}.png");
            return ResourceLoader.Exists(path) ? path : "card.png".CardImagePath();
        }
    }
    
    public override string BetaPortraitPath
    {
        get
        {
            var path = $"beta/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
            MegaCrit.Sts2.Core.Logging.Log.Info($"Sothis searching for {Id.Entry.RemovePrefix().ToLowerInvariant()}.png");
            return ResourceLoader.Exists(path) ? path : "card.png".CardImagePath();
        }
    }
}