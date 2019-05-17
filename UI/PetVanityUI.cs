﻿using Terraria.UI;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.UI.Chat;
using Microsoft.Xna.Framework.Graphics;
using System;
using AssortedCrazyThings.Items.PetAccessories;
using AssortedCrazyThings.Base;

namespace AssortedCrazyThings.UI
{
    class PetVanityUI : UIState
    {
        //Is the UI visible?
        internal static bool visible = false;
        //Spawn position, i.e. mouse position at UI start
        internal static Vector2 spawnPosition;
        internal static Vector2 leftCorner;

        //Circle diameter
        internal static int mainDiameter = 36;
        //Circle radius
        internal static int mainRadius = 36 / 2;

        //If pet currently has something of that type in that slot
        internal static bool hasEquipped = false;
        //Which thing is currently highlighted?
        internal static int returned = -1;

        //Fade in animation when opening the UI
        internal static float fadeIn = 0;

        //Red cross for when to unequip
        internal static Texture2D redCrossTexture;

        //Holds data about what to draw
        internal static PetAccessory petAccessory;

        //Initialization
        public override void OnInitialize()
        {
            spawnPosition = new Vector2();
            leftCorner = new Vector2();
            redCrossTexture = AssUtils.Instance.GetTexture("UI/UIRedCross");
        }

        //Update, unused
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        //Draw
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            Main.LocalPlayer.mouseInterface = true;

            //48
            int outerRadius = 48;
            if (petAccessory.AltTextures.Count > 5) outerRadius += 5 * (petAccessory.AltTextures.Count - 5); //increase by 5 after having more than 5 options, starts getting clumped at about 24 circles
            if (fadeIn < outerRadius) outerRadius = (int)(fadeIn += (float)outerRadius / 10);

            double offset = 0;
            double angleSteps = 2.0d / petAccessory.AltTextures.Count;
            int done;
            //Starting angle
            double i = offset;
            //done --> ID of currently drawn circle
            for (done = 0; done < petAccessory.AltTextures.Count; done++)
            {
                double x = outerRadius * Math.Sin(i * Math.PI);
                double y = outerRadius * -Math.Cos(i * Math.PI);
                

                Rectangle bgRect = new Rectangle((int)(leftCorner.X + x), (int)(leftCorner.Y + y), mainDiameter, mainDiameter);
                //Check if mouse is within the circle checked
                bool isMouseWithin = CircleUI.CheckMouseWithinWheel(Main.MouseScreen, spawnPosition, mainRadius, petAccessory.AltTextures.Count, done);

                //Actually draw the bg circle
                Color drawColor = Color.White;
                if (hasEquipped && done == petAccessory.Color)
                {
                    drawColor = Color.Gray;
                }
                spriteBatch.Draw(Main.wireUITexture[isMouseWithin ? 1 : 0], bgRect, drawColor);

                //Draw sprites over the icons
                int width = petAccessory.AltTextures[done].Width;
                int height = petAccessory.AltTextures[done].Height;
                Rectangle projRect = new Rectangle((int)(spawnPosition.X + x) - (width / 2), (int)(spawnPosition.Y + y) - (height / 2), width, height);

                Rectangle sourceRect = new Rectangle
                {
                    X = 0,
                    Y = 0,
                    Width = width,
                    Height = height
                };

                drawColor = Color.White;
                if (hasEquipped && done == petAccessory.Color) drawColor = Color.Gray;

                spriteBatch.Draw(petAccessory.AltTextures[done], projRect, sourceRect, drawColor);

                if (isMouseWithin)
                {
                    //set the "returned" new type
                    returned = done;
                    if (hasEquipped && done == petAccessory.Color) returned = -2;
                }

                i += angleSteps;
            }

            Texture2D bgTexture = Main.wireUITexture[0];

            //Draw held item bg circle
            Rectangle outputRect = new Rectangle((int)leftCorner.X, (int)leftCorner.Y, mainDiameter, mainDiameter);

            bool middle = CircleUI.CheckMouseWithinCircle(Main.MouseScreen, mainRadius, spawnPosition);

            spriteBatch.Draw(Main.wireUITexture[middle ? 1 : 0], outputRect, Color.White);

            //Draw held item inside circle
            if (petAccessory.Type != -1)
            {
                int finalWidth = Main.itemTexture[petAccessory.Type].Width;
                int finalHeight = Main.itemTexture[petAccessory.Type].Height;
                Rectangle outputWeaponRect = new Rectangle((int)spawnPosition.X - (finalWidth / 2), (int)spawnPosition.Y - (finalHeight / 2), finalWidth, finalHeight);
                //outputWeaponRect.Inflate(4, 4);
                spriteBatch.Draw(Main.itemTexture[petAccessory.Type], outputWeaponRect, Color.White);
            }

            if (middle)
            {
                //if hovering over the middle, reset color
                returned = -1;
                if (hasEquipped)
                {
                    //Draw the red cross
                    int finalWidth = redCrossTexture.Width;
                    int finalHeight = redCrossTexture.Height;
                    Rectangle outputCrossRect = new Rectangle((int)spawnPosition.X - (finalWidth / 2), (int)spawnPosition.Y - (finalHeight / 2), finalWidth, finalHeight);
                    spriteBatch.Draw(redCrossTexture, outputCrossRect, Color.White);

                    //Draw the tooltip
                    Color fontColor = Color.White;
                    Vector2 mousePos = new Vector2(Main.mouseX, Main.mouseY);
                    ChatManager.DrawColorCodedStringWithShadow(spriteBatch, Main.fontMouseText, "Unequip", mousePos + new Vector2(16, 16), fontColor, 0, Vector2.Zero, Vector2.One);
                }
            }

            //extra loop so tooltips are always drawn after the circles
            for (done = 0; done < petAccessory.AltTextures.Count; done++)
            {
                bool isMouseWithin = CircleUI.CheckMouseWithinWheel(Main.MouseScreen, spawnPosition, mainRadius, petAccessory.AltTextures.Count, done);
                string tooltip = petAccessory.AltTextureSuffixes[done];

                if (isMouseWithin)
                {
                    //Draw the tooltip
                    Color fontColor = Color.White;
                    Vector2 mousePos = new Vector2(Main.mouseX, Main.mouseY);
                    ChatManager.DrawColorCodedStringWithShadow(spriteBatch, Main.fontMouseText, tooltip, mousePos + new Vector2(16, 16), fontColor, 0, Vector2.Zero, Vector2.One);
                }
            }
        }
    }
}
