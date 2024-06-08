using Features.Saves.Gameplay.DTO.Balls;
using Features.Saves.Gameplay.DTO.Bonuses;
using Scenes.Gameplay.Feature.LevelCreation;
using System;

namespace Features.Saves.Gameplay
{
	[Serializable]
	public class GameplayData
	{
		public SavedPackData PackData;
		public LevelInfo LevelInfo;

		public BonusServiceState BonusServiceState;
		public BallsServiceState BallsServiceState;
	}
}
