using BaseLib.Abstracts;
using Sothis.SothisCode.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using Sothis.SothisCode.Cards;
using Sothis.SothisCode.Cards.Common;
using Sothis.SothisCode.Cards.Rare;
using Sothis.SothisCode.Cards.Uncommon;
using Conflagration = Sothis.SothisCode.Cards.Uncommon.Conflagration;
using Corruption = Sothis.SothisCode.Cards.Corruption;

namespace Sothis.SothisCode.Character;

public class SothisCardPool : CustomCardPoolModel
{
    public override string Title => Sothis.CharacterId; //This is not a display name.

    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();


    /* These HSV values will determine the color of your card back.
    They are applied as a shader onto an already colored image,
    so it may take some experimentation to find a color you like.
    Generally they should be values between 0 and 1. */
    public override float H => 0.65f; //Hue; changes the color.
    public override float S => 1f; //Saturation
    public override float V => 0.8f; //Brightness

    //Alternatively, leave these values at 1 and provide a custom frame image.
    /*public override Texture2D CustomFrame(CustomCardModel card)
    {
        //This will attempt to load Sothis/images/cards/frame.png
        return PreloadManager.Cache.GetTexture2D("cards/frame.png".ImagePath());
    }*/
    
    protected override CardModel[] GenerateAllCards()
    {
        return
        [
            (CardModel)ModelDb.Card<Corruption>(),
            (CardModel)ModelDb.Card<GlassDaggers>(),
            (CardModel)ModelDb.Card<MinorRelief>(),
            (CardModel)ModelDb.Card<Purification>(),
            (CardModel)ModelDb.Card<Sandshaping>(),
            
            (CardModel)ModelDb.Card<Concentration>(),
            
            (CardModel)ModelDb.Card<Conflagration>(),
            (CardModel)ModelDb.Card<GhastlyFire>(),
            (CardModel)ModelDb.Card<PurgingEruption>(),
            (CardModel)ModelDb.Card<SunRay>(),
            
            (CardModel)ModelDb.Card<FlammableChakram>()
        ];
    }

    //Color of small card icons
    public override Color DeckEntryCardColor => new("ffffff");

    public override bool IsColorless => false;
}