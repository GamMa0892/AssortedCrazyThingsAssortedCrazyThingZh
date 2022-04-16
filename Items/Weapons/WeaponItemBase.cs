using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace AssortedCrazyThings.Items.Weapons
{
	[Content(ContentType.Weapons)]
	public abstract class WeaponItemBase : AssItem
	{
		public sealed override void SetStaticDefaults()
		{
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			SafeSetStaticDefaults();
		}

		public virtual void SafeSetStaticDefaults()
		{

		}
	}
}
