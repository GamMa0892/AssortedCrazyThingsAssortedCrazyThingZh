using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace AssortedCrazyThings.Projectiles.Pets
{
    //check this file for more info vvvvvvvv
    public class OceanSlimeProj : BabySlimeBase
    {
		public override string Texture
		{
			get
			{
				return "AssortedCrazyThings/Projectiles/Pets/OceanSlimeProj_0";
			}
		}
		
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ocean Slime");
            Main.projFrames[projectile.type] = 6;
            Main.projPet[projectile.type] = true;
            drawOffsetX = 0;
            drawOriginOffsetY = 4;
        }

        public override void MoreSetDefaults()
        {
            //used to set dimensions and damage (if there is, defaults to 0)
            projectile.width = 52;
            projectile.height = 38;

            Damage = 0;
        }

        public override bool PreAI()
        {
            PetPlayer modPlayer = Main.player[projectile.owner].GetModPlayer<PetPlayer>(mod);
            if (Main.player[projectile.owner].dead)
            {
                modPlayer.OceanSlime = false;
            }
            if (modPlayer.OceanSlime)
            {
                projectile.timeLeft = 2;
            }
            return true;
        }
		
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			PetPlayer mPlayer = Main.player[projectile.owner].GetModPlayer<PetPlayer>(mod);
			SpriteEffects effects = projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
			Texture2D image = mod.GetTexture("Projectiles/Pets/OceanSlimeProj_" + mPlayer.oceanSlimeType);
			Rectangle bounds = new Rectangle
			{
				X = 0,
				Y = projectile.frame,
				Width = image.Bounds.Width,
				Height = image.Bounds.Height / Main.projFrames[projectile.type]
			};
			bounds.Y *= bounds.Height;

			Vector2 stupidOffset = new Vector2(projectile.width / 2, projectile.height / 2 + projectile.gfxOffY +4f);

			spriteBatch.Draw(image, projectile.position - Main.screenPosition + stupidOffset, bounds, lightColor, projectile.rotation, bounds.Size() / 2, projectile.scale, effects, 0f);

			return false;
		}
    }
}
