using AssortedCrazyThings.Base;
using Terraria;
using Terraria.ID;

namespace AssortedCrazyThings.Projectiles.Pets
{
	public class BabyIchorStickerProj : SimplePetProjBase
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Baby Ichor Sticker");
			Main.projFrames[Projectile.type] = 4;
			Main.projPet[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.BabyHornet);
			AIType = ProjectileID.BabyHornet;
			Projectile.width = 38;
			Projectile.height = 44;
		}

		public override bool PreAI()
		{
			Player player = Projectile.GetOwner();
			player.hornet = false; // Relic from AIType
			return true;
		}

		public override void AI()
		{
			Player player = Projectile.GetOwner();
			PetPlayer modPlayer = player.GetModPlayer<PetPlayer>();
			if (player.dead)
			{
				modPlayer.BabyIchorSticker = false;
			}
			if (modPlayer.BabyIchorSticker)
			{
				Projectile.timeLeft = 2;
			}
			AssAI.TeleportIfTooFar(Projectile, player.MountedCenter);

			if (Projectile.frameCounter % 2 == Main.GameUpdateCount % 2)
			{
				//Make it animate 50% slower by skipping every second increase
				Projectile.frameCounter--;
			}
		}
	}
}