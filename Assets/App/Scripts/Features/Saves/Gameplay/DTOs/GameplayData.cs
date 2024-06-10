using Features.Saves.Gameplay.DTO.Balls;
using Features.Saves.Gameplay.DTO.Bonuses;
using Features.Saves.Gameplay.DTO.Plate;
using Features.Saves.Gameplay.DTOs.Health;
using Features.Saves.Gameplay.DTOs.Level;
using Features.Saves.Gameplay.DTOs.Progress;
using System;

namespace Features.Saves.Gameplay
{
	[Serializable]
	public class GameplayData
	{
		public SavedPackData PackData;

		public LevelState LevelState;
		public ProgressState ProgressState;
		public HealthState HealthState;
		public PlateState PlateState;
		public BonusServiceState BonusServiceState;
		public BallsServiceState BallsServiceState;
	}
}
