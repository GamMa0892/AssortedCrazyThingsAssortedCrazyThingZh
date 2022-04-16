using AssortedCrazyThings.Buffs.Pets;
using AssortedCrazyThings.Projectiles.Pets;
using Terraria;
using Terraria.ModLoader;

namespace AssortedCrazyThings.Items.Pets
{
	[Content(ContentType.DroppedPets)]
	public class LilWrapsItem : SimplePetItemBase
	{
		public override int PetType => ModContent.ProjectileType<LilWrapsProj>();

		public override int BuffType => ModContent.BuffType<LilWrapsBuff>();

		public override void SafeSetStaticDefaults()
		{
			DisplayName.SetDefault("Gilded Coffin");
			Tooltip.SetDefault("Summons Lil' Wraps to follow you"
				+ "\nAppearance can be changed with Costume Suitcase");
		}

		public override void SafeSetDefaults()
		{
			Item.width = 20;
			Item.height = 26;
			Item.value = Item.sellPrice(silver: 10);
		}
	}
}
