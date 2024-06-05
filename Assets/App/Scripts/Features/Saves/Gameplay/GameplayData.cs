using Scenes.Gameplay.Feature.LevelCreation;
using System;
using System.Collections.Generic;

namespace Features.Saves.Gameplay
{
	[Serializable]
	public class GameplayData
	{
		public SavedPackData PackData;
		public LevelInfo LevelInfo;

		public List<BonusPosition> FallingBonusData;
		public List<ActiveBonus> ActiveBonusData;
		public List<BallData> BallsData;
	}
}
