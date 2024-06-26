using AssortedCrazyThings.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AssortedCrazyThings.Items.Placeable
{
	[Content(ContentType.PlaceablesFunctional | ContentType.DroppedPets | ContentType.OtherPets | ContentType.FriendlyNPCs, needsAllToFilterOut: true)]
	public class VanityDresserItem : PlaceableItem<VanityDresserTile>
	{
		public override LocalizedText Tooltip => ModContent.GetInstance<VanitySelector>().Tooltip;

		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(TileType);
			Item.width = 34;
			Item.height = 26;
			Item.maxStack = Item.CommonMaxStack;
			Item.rare = 1;
			Item.value = Item.sellPrice(silver: 10);
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.Dresser).AddIngredient(ModContent.ItemType<VanitySelector>()).Register();
		}
	}
}
