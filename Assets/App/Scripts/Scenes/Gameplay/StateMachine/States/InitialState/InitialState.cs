using Features.Saves;
using Features.StateMachine.States;
using Features.UI.SceneTransitions;
using Scenes.Gameplay.Feature.Health;
using Scenes.Gameplay.Feature.LevelCreation.Services;
using Scenes.Gameplay.Feature.UI;
using Scenes.PackSelection.Feature.Packs;

namespace Scenes.Gameplay.StateMachine.States
{
	public class InitialState : State
	{
		private IHealthController healthController;
		private ISceneTransition sceneTransition;
		private IPackProvider packProvider;
		private ILevelService levelService;
		private IPackInfoUI packInfoUI;

		private bool firstPlay = true;

		public InitialState(IHealthController healthController,
					  ISceneTransition sceneTransition,
					  ILevelService levelService,
					  IPackInfoUI packInfoUI,
					  IPackProvider packProvider)
		{
			this.healthController = healthController;
			this.sceneTransition = sceneTransition;
			this.levelService = levelService;
			this.packInfoUI = packInfoUI;
			this.packProvider = packProvider;
		}

		public override void Enter()
		{
			base.Enter();
			levelService.SetupLevel();
			healthController.ResetHealth();

			StateMachine.ChangeState<ResetState>();
			SetupUi();
		}

		public override void Exit()
		{
			base.Exit();
			if (firstPlay)
			{
				sceneTransition.PlayOff();
				firstPlay = false;
			}
		}

		private void SetupUi()
		{
			var currentPack = packProvider.CurrentPack;
			SavedPackData savedPackData = packProvider.SavedPackData;
			if (currentPack == null || savedPackData == null)
			{
				return;
			}
			packInfoUI.Init(currentPack.Sprite, savedPackData.CurrentLevel, currentPack.MaxLevel);
		}
	}
}
