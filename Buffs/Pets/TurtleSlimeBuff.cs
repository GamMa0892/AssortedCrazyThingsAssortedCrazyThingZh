using AssortedCrazyThings.Projectiles.Pets;
using Terraria;
using Terraria.ModLoader;

namespace AssortedCrazyThings.Buffs.Pets
{
	[Content(ContentType.FriendlyNPCs)]
	public class TurtleSlimeBuff : SimplePetBuffBase
	{
		public override int PetType => ModContent.ProjectileType<TurtleSlimeProj>();

		public override ref bool PetBool(Player player) => ref player.GetModPlayer<PetPlayer>().TurtleSlime;
	}

	[Content(ContentType.AommSupport | ContentType.FriendlyNPCs)]
	public class TurtleSlimeBuff_AoMM : SimplePetBuffBase_AoMM<TurtleSlimeBuff>
	{

	}
}
