using Features.Saves;
using Features.Saves.Gameplay.Providers;
using Features.StateMachine.States;
using Features.UI.SceneTransitions;
using Scenes.Gameplay.Feature.Bonuses.Provider;
using Scenes.Gameplay.Feature.Health;
using Scenes.Gameplay.Feature.LevelCreation.Saves;
using Scenes.Gameplay.Feature.LevelCreation.Services;
using Scenes.Gameplay.Feature.Reset.Services;
using Scenes.Gameplay.Feature.UI;
using Scenes.PackSelection.Feature.Packs;
using Scenes.PackSelection.Feature.Packs.Configs;
using System.Threading.Tasks;

namespace Scenes.Gameplay.StateMachine.States
{
	public class InitialState : State
	{
		private IHealthController healthController;
		private ISceneTransition sceneTransition;
		private IPackProvider packProvider;
		private ILevelService levelService;
		private IPackInfoUI packInfoUI;
		private IResetService resetService;
		private IBonusServicesProvider bonusServicesProvider;

		private ILevelSavingService levelSavingService;
		private IGameplaySavesProvider gameplaySavesProvider;

		private bool firstPlay = true;

		public InitialState(IHealthController healthController,
					  ISceneTransition sceneTransition,
					  ILevelService levelService,
					  IPackInfoUI packInfoUI,
					  IPackProvider packProvider,
					  IBonusServicesProvider bonusServicesProvider,
					  IResetService resetService,

					  ILevelSavingService levelSavingService,
					  IGameplaySavesProvider gameplaySavesProvider)
		{
			this.healthController = healthController;
			this.sceneTransition = sceneTransition;
			this.levelService = levelService;
			this.packInfoUI = packInfoUI;
			this.packProvider = packProvider;
			this.resetService = resetService;
			this.bonusServicesProvider = bonusServicesProvider;

			this.levelSavingService = levelSavingService;
			this.gameplaySavesProvider = gameplaySavesProvider;
		}

		public override void Enter()
		{
			base.Enter();
			EnterAsync();
		}

		private async void EnterAsync()
		{
			PlaySceneTransition();

			if (gameplaySavesProvider.IsContinue)
			{
				await LoadLevelToContinue();
				return;
			}
			await LoadNormalLevel();
		}

		private async Task LoadNormalLevel()
		{
			resetService.Reset();
			bonusServicesProvider.Cleanup();
			healthController.ResetHealth();

			if (packProvider.CurrentPack == null || packProvider.SavedPackData == null)
			{
				await levelService.SetupDefaultLevelAsync();
			}
			else
			{
				SetupUi(packProvider.CurrentPack, packProvider.SavedPackData);
				await levelService.SetupLevelFromPackAsync(packProvider.CurrentPack, packProvider.SavedPackData);
			}

			StateMachine.ChangeState<ResetState>();
		}

		private async Task LoadLevelToContinue()
		{
			await levelSavingService.LoadDataAsync();
			gameplaySavesProvider.IsContinue = false;
			SetupUi(packProvider.CurrentPack, packProvider.SavedPackData);
			StateMachine.ChangeState<GameplayState>();
		}

		private void PlaySceneTransition()
		{
			if (firstPlay)
			{
				sceneTransition.PlayOff();
				firstPlay = false;
			}
		}

		private void SetupUi(Pack currentPack, SavedPackData savedPackData)
		{
			packInfoUI.Init(currentPack.Sprite, savedPackData.CurrentLevel, currentPack.MaxLevel);
		}
	}
}
