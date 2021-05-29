using AssortedCrazyThings.Projectiles.Tools;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AssortedCrazyThings.Items.Tools
{
    public class ExtendoNetGolden : ExtendoNetBase
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Golden Extendo-Net");
            Tooltip.SetDefault("'Catches those REALLY hard to reach critters'");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.useAnimation = 18;
            Item.useTime = 24;
            Item.value = Item.sellPrice(gold: 5, silver: 90);
            Item.shoot = ModContent.ProjectileType<ExtendoNetGoldenProj>();
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ItemID.Wire, 10).AddRecipeGroup("ACT:GoldPlatinum", 10).AddIngredient(ItemID.GoldenBugNet, 1).AddTile(TileID.Anvils).Register();
        }
    }
}
