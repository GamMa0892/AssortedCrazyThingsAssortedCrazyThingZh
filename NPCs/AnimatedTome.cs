using AssortedCrazyThings.Base;
using AssortedCrazyThings.Items.Pets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace AssortedCrazyThings.NPCs
{
	//this version of "retexture" is old and not recommended, refer to DemonEyeFractured or similar

	[Content(ContentType.FriendlyNPCs)]
	public class AnimatedTome : AssNPC
	{
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[NPC.type] = 5;
			Main.npcCatchable[NPC.type] = true;

			NPCID.Sets.CountsAsCritter[NPC.type] = true; //Guide To Critter Companionship
		}

		public override void SetDefaults()
		{
			NPC.width = 26;
			NPC.height = 16;
			NPC.damage = 0;
			NPC.defense = 0;
			NPC.lifeMax = 5;
			NPC.dontTakeDamageFromHostiles = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.knockBackResist = 0.8f;
			NPC.aiStyle = 14;
			NPC.noGravity = true;
			AIType = NPCID.GiantBat;
			NPC.catchItem = ModContent.ItemType<AnimatedTomeItem>();
		}

		public override void FindFrame(int frameHeight)
		{
			NPC.LoopAnimation(frameHeight, 8);
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.DungeonNormal.Chance * 0.01f;
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.Common(ItemID.Book));
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon,
			});
		}

		public override void HitEffect(NPC.HitInfo hit)
		{
			if (Main.netMode == NetmodeID.Server)
			{
				return;
			}

			if (NPC.life <= 0)
			{
				var entitySource = NPC.GetSource_Death();
				for (int u = 0; u < 7; u++)
				{
					Vector2 pos = NPC.position + new Vector2(Main.rand.Next(NPC.width), Main.rand.Next(NPC.height));
					Gore gore = Gore.NewGoreDirect(entitySource, pos, NPC.velocity * 0.5f, Mod.Find<ModGore>("PaperScrapGore").Type, 1f);
					gore.velocity += new Vector2(Main.rand.NextFloat(3) - 1.5f, Main.rand.NextFloat(MathHelper.TwoPi) - 0.3f);
				}

				for (int i = 0; i < 3; i++)
				{
					Gore gore = Gore.NewGoreDirect(entitySource, NPC.position, NPC.velocity.SafeNormalize(Vector2.UnitY) * 3f, Mod.Find<ModGore>("PaperGore").Type, 1f + Main.rand.NextFloatDirection() * 0.2f);
					gore.velocity += new Vector2(Main.rand.NextFloat(2) - 1f, Main.rand.NextFloat(MathHelper.TwoPi) - 0.3f);
					gore.velocity *= 4f;
				}
			}
		}

		public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			int i = NPC.whoAmI % 5; //needs to be fixed per NPC instance
			if (i < 4)
			{
				Texture2D texture = Mod.Assets.Request<Texture2D>("NPCs/AnimatedTome_" + i).Value;
				Vector2 stupidOffset = new Vector2(0f, 0f); //4f
				SpriteEffects effect = NPC.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
				Vector2 drawPos = NPC.Center - screenPos - Vector2.Zero + stupidOffset;
				spriteBatch.Draw(texture, drawPos, NPC.frame, NPC.GetAlpha(drawColor), NPC.rotation, NPC.frame.Size() / 2, NPC.scale, effect, 0f);
			}
		}

		public override void PostAI()
		{
			NPC.spriteDirection = NPC.direction;
			NPC.rotation = NPC.velocity.X * 0.06f;

			if (Main.rand.NextBool(10))
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, 15);
			}
		}
	}
}
