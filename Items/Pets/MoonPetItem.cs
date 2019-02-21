using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AssortedCrazyThings.Items.Pets
{
    public class MoonPetItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bottled Moon");
            Tooltip.SetDefault("Summons a small moon that provides you with constant moonlight"
                + "\nShows the current moon cycle in the buff tip");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.ZephyrFish);
            item.shoot = mod.ProjectileType("MoonPetProj");
            item.buffType = mod.BuffType("MoonPetBuff");
            item.width = 20;
            item.height = 26;
            item.rare = -11;
            item.value = Item.sellPrice(copper: 10);
        }

        public override void UseStyle(Player player)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(item.buffType, 3600, true);
            }
        }

        //TODO add recipe
        public override void AddRecipes()
        {
            //ModRecipe recipe = new ModRecipe(mod);
            //recipe.AddIngredient(ItemID.TaxCollectorsStickOfDoom, 1);
            //recipe.AddTile(TileID.DemonAltar);
            //recipe.SetResult(this);
            //recipe.AddRecipe();
        }
    }
}
