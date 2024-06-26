using AssortedCrazyThings.Base;
using AssortedCrazyThings.Base.ModSupport.AoMM;
using AssortedCrazyThings.Buffs.Pets;
using Terraria;
using Terraria.ModLoader;

namespace AssortedCrazyThings.Projectiles.Pets
{
	[Content(ContentType.FriendlyNPCs)]
	//check this file for more info vvvvvvvv
	public class MeatballSlimeProj : BabySlimeBase
	{
		public override void SafeSetStaticDefaults()
		{
			AmuletOfManyMinionsApi.RegisterSlimePet(this, ModContent.GetInstance<MeatballSlimeBuff_AoMM>(), null);
		}

		public override void SafeSetDefaults()
		{
			Projectile.width = 34;
			Projectile.height = 30;
			Projectile.alpha = 0;

			Projectile.minion = false;
		}

		public override bool PreAI()
		{
			PetPlayer modPlayer = Projectile.GetOwner().GetModPlayer<PetPlayer>();
			if (Projectile.GetOwner().dead)
			{
				modPlayer.MeatballSlime = false;
			}
			if (modPlayer.MeatballSlime)
			{
				Projectile.timeLeft = 2;
			}
			return true;
		}
	}
}
