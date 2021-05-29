using AssortedCrazyThings.Base;
using AssortedCrazyThings.Buffs;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AssortedCrazyThings.Items.Consumables
{
    public class CuteSlimeSpawnEnableFlask : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jellied Ale");
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string tooltip = "You will see Cute Slimes more often for a short time";
            if (AssUtils.AssConfig.CuteSlimesPotionOnly)
            {
                tooltip = "Allows you to see Cute Slimes for a short time";
            }
            tooltips.Add(new TooltipLine(Mod, "Tooltip", tooltip));
        }

        public override void SetDefaults()
        {
            //item.CloneDefaults(ItemID.Silk);
            Item.width = 20;
            Item.height = 28;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = 30;
            Item.consumable = true;
            Item.buffTime = 18000; //five minutes
            Item.buffType = ModContent.BuffType<CuteSlimeSpawnEnableBuff>();
            Item.rare = -11;
            Item.value = Item.sellPrice(copper: 20);
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ItemID.Ale, 1).AddIngredient(ItemID.Gel, 1).AddTile(TileID.Kegs).Register();
        }
    }
}
