using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Harblesnargits_Mod_01.Items.Pets
{
	public class pet_Eye_09 : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Unconscious Mechanical Eye");
			Tooltip.SetDefault("Summons a docile purple Mechanical Eye to follow you.");
		}

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.ZephyrFish);
			item.shoot = mod.ProjectileType("pet_Eye_09");
			item.buffType = mod.BuffType("pet_Eye_09");
			item.rare = -11;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.BlackLens, 1);
			recipe.AddIngredient(ItemID.Lens, 2);
            recipe.AddTile(TileID.DemonAltar);  //WorkBenches, Anvils, MythrilAnvil, Furnaces, DemonAltar, or TinkerersWorkbench
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override void UseStyle(Player player)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(item.buffType, 3600, true);
			}
		}
	}
}