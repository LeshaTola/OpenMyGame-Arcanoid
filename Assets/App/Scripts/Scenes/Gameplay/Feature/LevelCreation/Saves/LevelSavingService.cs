using Features.Saves.Gameplay;
using Features.Saves.Gameplay.Providers;
using Module.Saves;
using Scenes.Gameplay.Feature.Bonuses.Provider;
using Scenes.Gameplay.Feature.LevelCreation.Services;
using Scenes.Gameplay.Feature.Player;
using Scenes.Gameplay.Feature.Player.Ball.Services;
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

		public LevelSavingService(IDataProvider<GameplayData> dataProvider,
							IGameplaySavesProvider gameplaySavesProvider,
							ILevelService levelService,
							IPackProvider packProvider,
							IBallService ballService,
							IBonusServicesProvider bonusServicesProvider,
							Plate plate)
		{
			this.dataProvider = dataProvider;
			this.levelService = levelService;
			this.packProvider = packProvider;
			this.ballService = ballService;
			this.bonusServicesProvider = bonusServicesProvider;
			this.gameplaySavesProvider = gameplaySavesProvider;
			this.plate = plate;

			gameplaySavesProvider.OnSave += OnSave;
			gameplaySavesProvider.OnLoad += OnLoad;
		}

		public void SaveData()
		{
			GameplayData gameplayData = new GameplayData()
			{
				LevelState = levelService.GetLevelState(),
				BonusServiceState = bonusServicesProvider.GetBonusServiceState(),
				BallsServiceState = ballService.GetBallServiceState(),
				PackData = packProvider.SavedPackData,
				PlateState = plate.GetPlateState(),
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

			levelService.SetLevelState(loadedGameplayData.LevelState);
			LoadPackProvider(loadedGameplayData);
			bonusServicesProvider.SetBonusServiceState(loadedGameplayData.BonusServiceState);
			ballService.SetBallServiceState(loadedGameplayData.BallsServiceState);
			plate.SetPlateState(loadedGameplayData.PlateState);
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

		private void OnLoad()
		{
			LoadData();
		}
	}
}
