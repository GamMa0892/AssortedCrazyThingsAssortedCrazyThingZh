﻿using Terraria.ID;

namespace AssortedCrazyThings.Base.Chatter.Conditions
{
	public class QueenBeeGenericChatterCondition : ChatterCondition
	{
		protected override bool Check(ChatterSource source, IChatterParams param)
		{
			return MatchesNPCType(param, NPCID.QueenBee);
		}
	}
}
