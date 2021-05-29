using AssortedCrazyThings.Tiles;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AssortedCrazyThings.Items.Placeable
{
    public class SlimeBeaconItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slime Beacon");
            Tooltip.SetDefault("'Do The Slime With Me!'");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.rare = -11;
            Item.value = Item.buyPrice(0, 10, 0, 0);
            Item.createTile = ModContent.TileType<SlimeBeaconTile>();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient) tooltips.Add(new TooltipLine(Mod, "Multi", "[c/FFA01D:DOES NOT WORK IN MULTIPLAYER]"));
        }
    }
}
