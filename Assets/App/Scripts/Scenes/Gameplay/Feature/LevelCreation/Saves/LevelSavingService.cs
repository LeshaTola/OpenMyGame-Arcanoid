using Features.Saves.Gameplay;
using Module.Saves;
using Scenes.Gameplay.Feature.Bonuses.Provider;
using Scenes.Gameplay.Feature.LevelCreation.Providers.Level;
using Scenes.Gameplay.Feature.Player;
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
		private IBonusServicesProvider bonusServicesProvider;
		private Plate plate;

		private LevelInfo loadedLevelInfo;

		public LevelInfo LoadedLevelInfo { get => loadedLevelInfo; }

		public LevelSavingService(ILevelProvider levelProvider,
							IPackProvider packProvider,
							IBallService ballService,
							IBonusServicesProvider bonusServicesProvider,
							Plate plate)
		{
			this.levelProvider = levelProvider;
			this.packProvider = packProvider;
			this.ballService = ballService;
			this.bonusServicesProvider = bonusServicesProvider;
			this.plate = plate;
		}

		public void SaveData()
		{
			GameplayData gameplayData = new GameplayData()
			{
				LevelInfo = levelProvider.GetCurrentLevelStateInfo(),
				BonusServiceState = bonusServicesProvider.GetBonusServiceState(),
				BallsServiceState = ballService.GetBallServiceState(),
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

			LoadLevelInfo(loadedGameplayData);
			LoadPackProvider(loadedGameplayData);
			bonusServicesProvider.SetBonusServiceState(loadedGameplayData.BonusServiceState);
			ballService.SetBallServiceState(loadedGameplayData.BallsServiceState);
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
	}
}
