using AssortedCrazyThings.Buffs;
using AssortedCrazyThings.Projectiles.Pets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AssortedCrazyThings.Items.Pets
{
    public class HornedSlimeItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bottled Horned Slime");
            Tooltip.SetDefault("Summons a friendly Horned Slime to follow you");
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ZephyrFish);
            Item.shoot = ModContent.ProjectileType<HornedSlimeProj>();
            Item.buffType = ModContent.BuffType<HornedSlimeBuff>();
            Item.rare = -11;
            Item.value = Item.sellPrice(copper: 10);
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(Item.buffType, 3600, true);
            }
        }
    }
}
