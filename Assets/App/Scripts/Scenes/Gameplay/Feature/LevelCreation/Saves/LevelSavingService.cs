using Features.Saves.Gameplay;
using Module.Saves;
using Scenes.Gameplay.Feature.Bonuses;
using Scenes.Gameplay.Feature.Bonuses.Services;
using Scenes.Gameplay.Feature.LevelCreation.Providers.Level;
using Scenes.Gameplay.Feature.Player;
using Scenes.Gameplay.Feature.Player.Ball;
using Scenes.Gameplay.Feature.Player.Ball.Services;
using Scenes.PackSelection.Feature.Packs;

namespace Scenes.Gameplay.Feature.LevelCreation.Saves
{
	public class LevelSavingService : ILevelSavingService
	{
		private IDataProvider<GameplayData> dataProvider;

		private ILevelProvider levelProvider;
		private IPackProvider packProvider;
		private IBallService ballService;
		private IBonusService bonusService;
		private Plate plate;

		private LevelInfo loadedLevelInfo;

		public LevelInfo LoadedLevelInfo { get => loadedLevelInfo; }

		public LevelSavingService(ILevelProvider levelProvider,
							IPackProvider packProvider,
							IBallService ballService,
							IBonusService bonusService,
							Plate plate)
		{
			this.levelProvider = levelProvider;
			this.packProvider = packProvider;
			this.ballService = ballService;
			this.bonusService = bonusService;
			this.plate = plate;
		}

		public void SaveData()
		{
			GameplayData gameplayData = new GameplayData()
			{
				LevelInfo = levelProvider.GetCurrentLevelStateInfo(),
				FallingBonusData = bonusService.GetBonusesPositions(),
				ActiveBonusData = bonusService.GetActiveBonuses(),
				BallsData = ballService.GetBallsData(),
			};


			dataProvider.SaveData(gameplayData);
		}

		public void LoadData()
		{
			GameplayData loadedGameplayData = dataProvider.GetData();
			if (loadedGameplayData == null)
			{
				return;
			}

			LoadFallingBonuses(loadedGameplayData);
			LoadActiveBonuses(loadedGameplayData);
			LoadLevelInfo(loadedGameplayData);
			LoadPackProvider(loadedGameplayData);
			LoadBalls(loadedGameplayData);

		}

		private void LoadFallingBonuses(GameplayData loadedGameplayData)
		{
			foreach (BonusPosition bonusData in loadedGameplayData.FallingBonusData)
			{
				Bonus bonus = bonusService.GetBonus(bonusData.Id);
				bonus.transform.position = bonusData.Position;
			}
		}

		private void LoadActiveBonuses(GameplayData loadedGameplayData)
		{
			foreach (var bonusData in loadedGameplayData.ActiveBonusData)
			{
				var bonusCommand = bonusService.GetBonusCommand(bonusData.Id);
				bonusService.StartBonus(bonusCommand);
				bonusCommand.Timer = bonusData.RemainingTime;
			}
		}

		private void LoadLevelInfo(GameplayData loadedGameplayData)
		{
			loadedLevelInfo = loadedGameplayData.LevelInfo;
		}

		private void LoadPackProvider(GameplayData loadedGameplayData)
		{
			packProvider.SavedPackData = loadedGameplayData.PackData;
			packProvider.PackIndex = packProvider.Packs.FindIndex(x => x.Id.Equals(loadedGameplayData.PackData.Id));
		}

		private void LoadBalls(GameplayData loadedGameplayData)
		{
			foreach (BallData ballData in loadedGameplayData.BallsData)
			{
				Ball newBall = ballService.GetBall();
				newBall.transform.position = ballData.Position;
				newBall.Movement.Push(ballData.Direction);

				if (!ballData.IsOnPlate)
				{
					return;
				}
				newBall.Movement.Rb.simulated = false;
				newBall.transform.SetParent(plate.transform);
			}
		}
	}
}
