using AssortedCrazyThings.Base;
using Terraria;
using Terraria.ModLoader;

namespace AssortedCrazyThings.Projectiles.Pets
{
    [Content(ContentType.FriendlyNPCs)]
    //check this file for more info vvvvvvvv
    public class JoyousSlimeProj : BabySlimeBase
    {
        public override void SafeSetStaticDefaults()
        {
            DisplayName.SetDefault("Joyous Slime");
        }

        public override void SafeSetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 30;

            Projectile.minion = false;
        }

        public override bool PreAI()
        {
            PetPlayer modPlayer = Projectile.GetOwner().GetModPlayer<PetPlayer>();
            if (Projectile.GetOwner().dead)
            {
                modPlayer.JoyousSlime = false;
            }
            if (modPlayer.JoyousSlime)
            {
                Projectile.timeLeft = 2;
            }
            return true;
        }
    }
}
