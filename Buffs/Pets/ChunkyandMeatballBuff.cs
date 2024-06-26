using AssortedCrazyThings.Projectiles.Pets;
using Terraria;
using Terraria.ModLoader;

namespace AssortedCrazyThings.Buffs.Pets
{
	[Content(ContentType.FriendlyNPCs)]
	public class ChunkyandMeatballBuff : SimplePetBuffBase
	{
		public override int PetType => ModContent.ProjectileType<ChunkyProj>();

		public override ref bool PetBool(Player player) => ref player.GetModPlayer<PetPlayer>().ChunkyandMeatball;

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffTime[buffIndex] = 18000;
			PetBool(player) = true;
			if (player.whoAmI == Main.myPlayer)
			{
				var spawnSource = player.GetSource_Buff(buffIndex);

				int chunky = ModContent.ProjectileType<ChunkyProj>();
				bool chunkyNotSpawned = player.ownedProjectileCounts[chunky] <= 0;
				int meatball = ModContent.ProjectileType<MeatballProj>();
				bool meatballNotSpawned = player.ownedProjectileCounts[meatball] <= 0;
				if (chunkyNotSpawned)
				{
					Projectile.NewProjectile(spawnSource, player.Center.X, player.Top.Y - 6f, player.direction * 0.75f, -0.5f, chunky, 0, 0f, player.whoAmI);
				}
				if (meatballNotSpawned)
				{
					Projectile.NewProjectile(spawnSource, player.Center.X, player.Bottom.Y + 6f, player.direction * 0.75f, 0.5f, meatball, 0, 0f, player.whoAmI);
				}
			}
		}
	}

	[Content(ContentType.AommSupport | ContentType.FriendlyNPCs)]
	public class ChunkyandMeatballBuff_AoMM : SimplePetBuffBase_AoMM<ChunkyandMeatballBuff>
	{

	}
}
