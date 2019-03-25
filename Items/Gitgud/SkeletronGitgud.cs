using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AssortedCrazyThings.Items.Gitgud
{
	public class SkeletronGitgud : ModItem
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Carton of Soy Milk");
			Tooltip.SetDefault("15% reduced damage taken from Skeletron"
                + "\nImmunity to bleeding while Skeletron is alive"
                + "\n[c/E180CE:'git gud']");
		}

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.Silk);
            item.width = 32;
            item.height = 32;
            item.value = Item.sellPrice(copper: 1);
            item.rare = -1;
            item.maxStack = 1;
            item.accessory = true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<GitGudPlayer>(mod).skeletronGitgud = true;
        }
    }
}