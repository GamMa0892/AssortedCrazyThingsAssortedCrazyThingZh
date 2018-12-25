﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AssortedCrazyThings.NPCs.DungeonBird
{
    public class aaaSoul : ModNPC
    {
        public static string name = "aaaSoul";
        public static int wid = 24;
        public static int hei = 38;

        //public override string Texture
        //{
        //    get
        //    {
        //        return "AssortedCrazyThings/NPCs/CuteSlimeBlack"; //temp
        //    }
        //}

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(name);
            Main.npcFrameCount[npc.type] = 8;
        }

        public override void SetDefaults()
        {
            //adjust stats here to match harvester hitbox 1:1, then do findframes in postdraw
            npc.width = wid; //42 //16
            npc.height = hei; //52 //24
            npc.dontTakeDamageFromHostiles = true;
            npc.friendly = true;
            npc.noGravity = true;
            npc.damage = 7;
            npc.defense = 2;
            npc.lifeMax = 5;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 25f;
            npc.knockBackResist = 0.9f;
            npc.aiStyle = -1;
            aiType = -1;// NPCID.ToxicSludge;
            animationType = -1;// NPCID.ToxicSludge;
            npc.color = new Color(0, 0, 0, 50);
            Main.npcCatchable[mod.NPCType(name)] = true;
            npc.catchItem = (short)ItemID.SandBlock;
            npc.timeLeft = NPC.activeTime * 5;
        }

        private short offsetYPeriod = 120;

        public float AI_State
        {
            get
            {
                return npc.ai[0];
            }
            set
            {
                npc.ai[0] = value;
            }
        }

        public float AI_Local_Timer
        {
            get
            {
                return npc.localAI[0];
            }
            set
            {
                npc.localAI[0] = value;
            }
        }
        
        private void KillInstantly()
        {
            npc.life = 0;
            npc.active = false;
            npc.netUpdate = true;
        }

        public override bool CheckActive()
        {
            //manually decrease timeleft
            return false;
        }

        protected int HarvesterTarget()
        {
            int tar = 200;
            for (short j = 0; j < 200; j++)
            {
                if (Main.npc[j].active && Array.IndexOf(AssWorld.harvesterTypes, Main.npc[j].type) != -1)
                {
                    tar = j;
                    break;
                }
            }
            return tar;
        }

        protected Entity GetTarget()
        {
            int res = HarvesterTarget();
            if (res != 200) return Main.npc[res];
            else return npc;
        }

        protected bool IsTargetActive()
        {
            return !GetTarget().Equals(npc);
        }

        protected void SetTimeLeft()
        {
            NPC tar = (NPC)GetTarget();
            if (!tar.Equals(npc))
            {
                if (tar.active && Array.IndexOf(AssWorld.harvesterTypes, tar.type) != -1) //type check since souls might despawn and index changes
                {
                    npc.timeLeft = BaseHarvester.EatTimeConst;
                    Main.NewText("set time left to " + BaseHarvester.EatTimeConst);
                    npc.netUpdate = true;
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (AI_State == 0)
            {
                npc.frameCounter++;
                if (npc.frameCounter <= 8.0)
                {
                    npc.frame.Y = 0;
                }
                else if (npc.frameCounter <= 16.0)
                {
                    npc.frame.Y = frameHeight * 1;
                }
                else if (npc.frameCounter <= 24.0)
                {
                    npc.frame.Y = frameHeight * 2;
                }
                else if (npc.frameCounter <= 32.0)
                {
                    npc.frame.Y = frameHeight * 3;
                }
                else
                {
                    npc.frameCounter = 0;
                }
            }
            else if (AI_State == 1)
            {

                //Main.NewText(npc.velocity.X);
                if (npc.velocity.Y > 0/* && Math.Abs(npc.velocity.X) < 1f*/) //dropping down
                {
                    npc.frame.Y = frameHeight * 4;
                }
                else if ((npc.velocity.Y == 0 || npc.velocity.Y < 2f && npc.velocity.Y > 0f) && npc.velocity.X == 0)
                {
                    npc.frameCounter++;
                    if (npc.frameCounter <= 8.0)
                    {
                        npc.frame.Y = frameHeight * 5;
                    }
                    else if (npc.frameCounter <= 16.0)
                    {
                        npc.frame.Y = frameHeight * 6;
                    }
                    else if (npc.frameCounter <= 24.0)
                    {
                        npc.frame.Y = frameHeight * 7;
                    }
                    else if (npc.frameCounter <= 32.0)
                    {
                        npc.frame.Y = frameHeight * 6;
                    }
                    else
                    {
                        npc.frameCounter = 0;
                    }
                }
            }
            else
            {
                npc.frame.Y = 0;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            return false;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            lightColor.R = Math.Max(lightColor.R, (byte)100);
            lightColor.G = Math.Max(lightColor.G, (byte)100);
            lightColor.B = Math.Max(lightColor.B, (byte)100);
            lightColor.A = 255;

            SpriteEffects effects = SpriteEffects.None;
            Texture2D image = Main.npcTexture[npc.type];
            Rectangle bounds = new Rectangle
            {
                X = 0,
                Y = npc.frame.Y,
                Width = image.Bounds.Width,
                Height = (int)(image.Bounds.Height / Main.npcFrameCount[npc.type])
            };

            float sinY = 0;
            if (Main.netMode != NetmodeID.Server)
            {
                if (AI_State == 0)
                {
                    AI_Local_Timer = AI_Local_Timer > offsetYPeriod ? 0 : AI_Local_Timer + 1;
                    sinY = (float)((Math.Sin((AI_Local_Timer / offsetYPeriod) * 2 * Math.PI) - 1) * 10);
                }
                else if (AI_State == 1)
                {
                    if (AI_Local_Timer != 0.25f * offsetYPeriod && AI_Local_Timer != 1.25f * offsetYPeriod) //zero at 1/4 and 5/4 PI
                    {
                        AI_Local_Timer++;
                        sinY = (float)((Math.Sin((AI_Local_Timer / offsetYPeriod) * 2 * Math.PI) - 1) * 10);
                    }
                    else
                    {
                        sinY = 0;
                    }
                }
            }

            Vector2 stupidOffset = new Vector2(wid/2, (hei - 10f)+ sinY);
            spriteBatch.Draw(image, npc.position - Main.screenPosition + stupidOffset, bounds, lightColor, npc.rotation, bounds.Size() / 2, npc.scale, effects, 0f);
        }

        public override void AI()
        {
            Entity tar = GetTarget();
            NPC tarnpc = new NPC();
            if (tar is NPC)
            {
                tarnpc = (NPC)tar;
            }

            npc.direction = 1;
            //Main.NewText("" + AI_State + "" + npc.velocity);
            npc.noTileCollide = false;
            Lighting.AddLight(npc.Center, new Vector3(0.15f, 0.15f, 0.35f));

            if (AI_State == 0)
            {
                npc.noGravity = true;
                //npc.position - new Vector2(10f, 4f), npc.width + 20, npc.height + 4

                //concider only the bottom half of the hitbox, a bit wider (minus a small bit below)
                if (Collision.SolidCollision(npc.position + new Vector2(-10f, npc.height / 2), npc.width + 20, npc.height / 2 -2))
                {
                    if (IsTargetActive())
                    {
                        npc.noTileCollide = true;
                        Vector2 between = tarnpc.Center - npc.Center;
                        float factor = 2f; //2f
                        int acc = 100; //4
                        between.Normalize();
                        between *= factor;
                        npc.velocity = (npc.velocity * (acc - 1) + between) / acc;
                    }
                }
                else
                {
                    npc.noTileCollide = false;
                    npc.velocity *= 0.1f;
                }
            }

            if (!tarnpc.Equals(npc))
            {
                if (npc.getRect().Intersects(tarnpc.getRect()) && AI_State == 0 && !Collision.SolidCollision(npc.position, npc.width, npc.height -2)/* && tarnpc.velocity.Y <= 0*/) // tarnpc.velocity.Y <= 0 for only when it jumps
                {
                    AI_State = 1;
                    SetTimeLeft();
                    npc.velocity.Y = 1f;
                }
                //else if(!npc.getRect().Intersects(tarnpc.getRect()) && AI_State == 1 &&
                //(npc.velocity.Y == 0 || (npc.velocity.Y < 2f && npc.velocity.Y > 0f)))
                //{
                //    Main.NewText("test22222");
                //    npc.velocity.X = 0f;
                //}
            }

            if(AI_State == 1 && npc.velocity.Y != 0)
            {
                float betweenX = tarnpc.Center.X - npc.Center.X;
                if (betweenX > 2f || betweenX < -2f)
                {
                    float factor = 5f; //2f
                    int acc = 2; //4
                    betweenX = betweenX / Math.Abs(betweenX);
                    betweenX *= factor;
                    npc.velocity.X = (npc.velocity.X * (acc - 1) + betweenX) / acc;
                    npc.noTileCollide = false;
                }
                else
                {
                    npc.velocity.X = 0;
                }
                npc.velocity.Y += 0.06f;
            }
            else if (AI_State == 1 && (npc.velocity.Y == 0 || npc.velocity.Y < 2f && npc.velocity.Y > 0f))
            {
                npc.velocity.X = 0;
            }
            //remove this in actual release, only --npc.timeLeft
            /*if (AI_State == 1)*/--npc.timeLeft;
            if (npc.timeLeft < 0)
            {
                KillInstantly();
            }
        }
    }
}