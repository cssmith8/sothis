using BaseLib.Abstracts;
using BaseLib.Utils;
using Sothis.SothisCode.Character;

namespace Sothis.SothisCode.Potions;

[Pool(typeof(SothisPotionPool))]
public abstract class SothisPotion : CustomPotionModel;