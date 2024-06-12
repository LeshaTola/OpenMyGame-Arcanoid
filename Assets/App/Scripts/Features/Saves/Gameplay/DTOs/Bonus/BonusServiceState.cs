using System;
using System.Collections.Generic;

namespace Features.Saves.Gameplay.DTO.Bonuses
{
	[Serializable]
	public struct BonusServiceState
	{
		public List<BonusCommandData> BonusesCommandsData;
		public List<BonusPosition> BonusData;
	}
}
