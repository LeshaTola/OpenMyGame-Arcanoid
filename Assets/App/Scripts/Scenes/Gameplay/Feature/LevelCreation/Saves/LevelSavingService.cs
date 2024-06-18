using Cysharp.Threading.Tasks;
using Features.Saves.Gameplay;
using Features.Saves.Gameplay.Providers;
using Module.Saves;
using Scenes.Gameplay.Feature.Bonuses.Provider;
using Scenes.Gameplay.Feature.Health;
using Scenes.Gameplay.Feature.LevelCreation.Services;
using Scenes.Gameplay.Feature.Player;
using Scenes.Gameplay.Feature.Player.Ball.Services;
using Scenes.Gameplay.Feature.Progress;
using Scenes.PackSelection.Feature.Packs;

namespace Scenes.Gameplay.Feature.LevelCreation.Saves
{
	public class LevelSavingService : ILevelSavingService
	{
		private IDataProvider<GameplayData> dataProvider;

		private ILevelService levelService;
		private IPackProvider packProvider;
		private IBallService ballService;
		private IBonusServicesProvider bonusServicesProvider;
		private IGameplaySavesProvider gameplaySavesProvider;
		private Plate plate;

		private IHealthController healthController;
		private IProgressController progressController;

		public LevelSavingService(IDataProvider<GameplayData> dataProvider,
							IGameplaySavesProvider gameplaySavesProvider,
							ILevelService levelService,
							IPackProvider packProvider,
							IBallService ballService,
							IBonusServicesProvider bonusServicesProvider,
							IHealthController healthController,
							IProgressController progressController,
							Plate plate)
		{
			this.dataProvider = dataProvider;
			this.levelService = levelService;
			this.packProvider = packProvider;
			this.ballService = ballService;
			this.bonusServicesProvider = bonusServicesProvider;
			this.gameplaySavesProvider = gameplaySavesProvider;
			this.healthController = healthController;
			this.progressController = progressController;
			this.plate = plate;

			gameplaySavesProvider.OnSave += OnSave;
			gameplaySavesProvider.OnLoad += OnLoad;
		}

		public void SaveData()
		{
			GameplayData gameplayData = new GameplayData()
			{
				LevelState = levelService.GetLevelState(),
				ProgressState = progressController.GetProgressState(),
				HealthState = healthController.GetHealthState(),
				BonusServiceState = bonusServicesProvider.GetBonusServiceState(),
				BallsServiceState = ballService.GetBallServiceState(),
				PackData = packProvider.SavedPackData,
				PlateState = plate.GetPlateState(),
			};

			dataProvider.SaveData(gameplayData);
		}

		public async UniTask LoadDataAsync()
		{
			GameplayData loadedGameplayData = dataProvider.GetData();
			if (loadedGameplayData == null)
			{
				return;
			}

			LoadPackProvider(loadedGameplayData);
			progressController.SetProgressState(loadedGameplayData.ProgressState);
			healthController.SetHealthState(loadedGameplayData.HealthState);
			plate.SetPlateState(loadedGameplayData.PlateState);
			ballService.SetBallServiceState(loadedGameplayData.BallsServiceState);
			bonusServicesProvider.SetBonusServiceState(loadedGameplayData.BonusServiceState);

			await levelService.SetLevelStateAsync(loadedGameplayData.LevelState);
			progressController.SetProgressState(loadedGameplayData.ProgressState);
			ballService.PushBalls();
		}

		public void DeleteData()
		{
			dataProvider.DeleteData();
		}

		public void Cleanup()
		{
			gameplaySavesProvider.OnSave -= OnSave;
			gameplaySavesProvider.OnLoad -= OnLoad;
		}

		private void LoadPackProvider(GameplayData loadedGameplayData)
		{
			packProvider.SavedPackData = loadedGameplayData.PackData;
			packProvider.PackIndex = packProvider.Packs.FindIndex(x => x.Id.Equals(loadedGameplayData.PackData.Id));
		}

		private void OnSave()
		{
			SaveData();
		}

		private async void OnLoad()
		{
			await LoadDataAsync();
		}
	}
}
