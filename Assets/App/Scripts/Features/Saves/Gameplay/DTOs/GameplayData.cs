using Features.Saves.Gameplay.DTO.Balls;
using Features.Saves.Gameplay.DTO.Bonuses;
using Features.Saves.Gameplay.DTO.Plate;
using Features.Saves.Gameplay.DTOs.Level;
using System;

namespace Features.Saves.Gameplay
{
	[Serializable]
	public class GameplayData
	{
		public SavedPackData PackData;

		public LevelState LevelState;
		public PlateState PlateState;
		public BonusServiceState BonusServiceState;
		public BallsServiceState BallsServiceState;
	}
}
