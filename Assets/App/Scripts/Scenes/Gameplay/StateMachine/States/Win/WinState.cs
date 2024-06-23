using Features.Energy.Providers;
using Features.Saves;
using Features.StateMachine.States;
using Scenes.Gameplay.Feature.LevelCreation.Saves;
using Scenes.Gameplay.Feature.Player;
using Scenes.Gameplay.Feature.Player.Ball.Services;
using Scenes.Gameplay.StateMachine.States.Win.Routers;
using Scenes.PackSelection.Feature.Packs;
using Scenes.PackSelection.Feature.Packs.Configs;

namespace Scenes.Gameplay.StateMachine.States.Win
{
	public class WinState : State
	{
		private IRouterShowWin routerShowWin;
		private IPackProvider packProvider;
		private IBallService ballService;
		private IEnergyProvider energyProvider;
		private ILevelSavingService levelSavingService;

		private Plate plate;

		public float BallsStopDuration;

		public WinState(IRouterShowWin routerShowWin,
				  IPackProvider packProvider,
				  IBallService ballService,
				  IEnergyProvider energyProvider,
				  ILevelSavingService levelSavingService,
				  Plate plate)
		{
			this.routerShowWin = routerShowWin;
			this.packProvider = packProvider;
			this.ballService = ballService;
			this.energyProvider = energyProvider;
			this.levelSavingService = levelSavingService;
			this.plate = plate;
		}

		public override void Enter()
		{
			base.Enter();
			levelSavingService.DeleteData();

			energyProvider.AddEnergy(energyProvider.Config.WinReward);
			EnterAsync();
		}

		private async void EnterAsync()
		{
			plate.Stop();

			await ballService.StopBallsAsync(BallsStopDuration);

			routerShowWin.ShowWin();
			ProcessPacks();
		}


		private void ProcessPacks()
		{
			SavedPackData savedPackData = packProvider.SavedPackData;
			Pack currentPack = packProvider.CurrentPack;
			if (savedPackData == null || currentPack == null)
			{
				return;
			}

			CompleteLevel(savedPackData);
			if (savedPackData.CurrentLevel > currentPack.MaxLevel)
			{
				OpenNextPack(savedPackData);
			}
			packProvider.SaveData();
		}

		private void CompleteLevel(SavedPackData savedPackData)
		{
			savedPackData.CurrentLevel++;
			packProvider.PacksData[savedPackData.Id] = savedPackData;
		}

		private void OpenNextPack(SavedPackData savedPackData)
		{
			savedPackData.IsCompeted = true;

			int nextPackIndex = packProvider.PackIndex - 1;
			if (nextPackIndex < 0)
			{
				return;
			}
			string nextPackID = packProvider.Packs[nextPackIndex].Id;
			var nextPackData = packProvider.PacksData[nextPackID];
			nextPackData.IsOpened = true;
			SetNextPack(nextPackData, nextPackIndex);
		}

		private void SetNextPack(SavedPackData nextPackData, int nextPackIndex)
		{
			packProvider.PackIndex = nextPackIndex;
			packProvider.SavedPackData = nextPackData;
		}
	}
}
