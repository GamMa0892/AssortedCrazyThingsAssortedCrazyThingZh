using AssortedCrazyThings.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AssortedCrazyThings.Projectiles.Pets
{
    public class QueenLarvaProj : ModProjectile
    {
        private int sincounter;

        public override string Texture
        {
            get
            {
                return "AssortedCrazyThings/Projectiles/Pets/QueenLarvaProj_0"; //temp
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Queen Bee Larva");
            Main.projFrames[Projectile.type] = 4;
            Main.projPet[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.DD2PetGhost);
            Projectile.aiStyle = -1;
            Projectile.width = 28;
            Projectile.height = 34;
            Projectile.alpha = 0;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            PetPlayer mPlayer = Projectile.GetOwner().GetModPlayer<PetPlayer>();
            Texture2D image = Mod.GetTexture("Projectiles/Pets/QueenLarvaProj_" + mPlayer.queenLarvaType).Value;
            Rectangle bounds = new Rectangle();
            bounds.X = 0;
            bounds.Width = image.Bounds.Width;
            bounds.Height = (image.Bounds.Height / Main.projFrames[Projectile.type]);
            bounds.Y = Projectile.frame * bounds.Height;

            sincounter = sincounter > 150 ? 0 : sincounter + 1;
            float sinY = (float)((Math.Sin((sincounter / 150f) * MathHelper.TwoPi) - 1) * 2);

            Vector2 stupidOffset = new Vector2(Projectile.width / 2, (Projectile.height - 20f) + sinY);
            Vector2 drawPos = Projectile.position - Main.screenPosition + stupidOffset;

            Main.spriteBatch.Draw(image, drawPos, bounds, lightColor, 0f, bounds.Size() / 2, 1f, SpriteEffects.None, 0f);
            return false;
        }

        public override void AI()
        {
            Player player = Projectile.GetOwner();
            PetPlayer modPlayer = player.GetModPlayer<PetPlayer>();
            if (player.dead)
            {
                modPlayer.QueenLarva = false;
            }
            if (modPlayer.QueenLarva)
            {
                Projectile.timeLeft = 2;

                AssAI.FlickerwickPetAI(Projectile, lightPet: false, lightDust: false, reverseSide: true, offsetX: 16f, offsetY: 10f);

                AssAI.FlickerwickPetDraw(Projectile, frameCounterMaxFar: 4, frameCounterMaxClose: 10);
            }
        }
    }
}
