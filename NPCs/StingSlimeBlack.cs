using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AssortedCrazyThings.NPCs
{
    public class StingSlimeBlack : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sting Slime");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.ToxicSludge];
        }

        public override void SetDefaults()
        {
            NPC.width = 40;
            NPC.height = 36;
            NPC.damage = 7;
            NPC.defense = 2;
            NPC.lifeMax = 25;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 25f;
            NPC.knockBackResist = 0.25f;
            NPC.aiStyle = 1;
            AIType = NPCID.ToxicSludge;
            AnimationType = NPCID.ToxicSludge;
            Main.npcCatchable[Mod.Find<ModNPC>("StingSlimeBlack").Type] = true;
            NPC.catchItem = (short)Mod.Find<ModItem>("StingSlimeBlackItem").Type;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.OverworldDayDesert.Chance * 0.2f;
        }

        public override void OnKill()
        {
            Item.NewItem(NPC.getRect(), ItemID.Stinger);
        }
    }
}
