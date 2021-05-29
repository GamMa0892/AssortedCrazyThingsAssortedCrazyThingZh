using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AssortedCrazyThings.Items.Accessories.Useful
{
    [AutoloadEquip(EquipType.Balloon)]
    public class StarWispBalloon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Star Wisp Balloon");
            Tooltip.SetDefault("Increased mana regeneration and jump height" +
                               "\nGlows in the dark");
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 32;
            Item.value = 0;
            Item.rare = -11;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //player.manaRegenDelayBonus++;
            //player.manaRegenBonus += 25;
            if (!player.HasBuff(BuffID.StarInBottle))
            {
                player.manaRegenBonus += 2;
            }
            player.jumpBoost = true;
            Lighting.AddLight(player.Center, 0.7f, 1.3f, 1.6f);
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient<StarInABalloon>().AddIngredient<WispInABalloon>().AddTile(TileID.TinkerersWorkbench).Register();
        }
    }
}
