using Terraria;

namespace AssortedCrazyThings.Items.DroneUnlockables
{
	[Content(ContentType.Weapons)]
	public class DroneParts : AssItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Drone Parts");
			// Tooltip.SetDefault("'These parts could be repurposed...'");

			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.rare = 4;
			Item.width = 26;
			Item.height = 24;
			Item.value = Item.sellPrice(silver: 50);
		}
	}
}
