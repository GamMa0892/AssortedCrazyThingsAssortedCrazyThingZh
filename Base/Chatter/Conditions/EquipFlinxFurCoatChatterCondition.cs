﻿using AssortedCrazyThings.Base.Data;
using Terraria.ID;

namespace AssortedCrazyThings.Base.Chatter.Conditions
{
	public class EquipFlinxFurCoatChatterCondition : ChatterCondition
	{
		protected override bool Check(ChatterSource source, IChatterParams param)
		{
			return JustEquipped(param, new EquipSnapshot(body: ArmorIDs.Body.FlinxFurCoat));
		}
	}
}
