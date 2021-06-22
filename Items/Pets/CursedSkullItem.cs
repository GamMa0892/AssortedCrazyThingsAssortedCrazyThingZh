using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AssortedCrazyThings.Buffs.Pets;
using AssortedCrazyThings.Projectiles.Pets;

namespace AssortedCrazyThings.Items.Pets
{
    [Autoload]
    [LegacyName("CursedSkull")]
    public class CursedSkullItem : SimplePetItemBase
    {
        public override int PetType => ModContent.ProjectileType<CursedSkullProj>();

        public override int BuffType => ModContent.BuffType<CursedSkullBuff>();

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Inert Skull");
            Tooltip.SetDefault("Summons a friendly cursed skull that follows you"
                + "\nAppearance can be changed with Costume Suitcase");
        }

        public override void SafeSetDefaults()
        {
            Item.rare = -11;
            Item.value = Item.sellPrice(copper: 10);
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ItemID.Bone, 10).AddTile(TileID.DemonAltar).Register();
        }
    }
}
