﻿using Features.Saves;
using Features.StateMachine.States;
using Features.UI.SceneTransitions;
using Scenes.Gameplay.Feature.Health;
using Scenes.Gameplay.Feature.LevelCreation.Services;
using Scenes.Gameplay.Feature.Reset.Services;
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
		private IResetService resetService;

		private bool firstPlay = true;

		public InitialState(IHealthController healthController,
					  ISceneTransition sceneTransition,
					  ILevelService levelService,
					  IPackInfoUI packInfoUI,
					  IPackProvider packProvider,
					  IResetService resetService)
		{
			this.healthController = healthController;
			this.sceneTransition = sceneTransition;
			this.levelService = levelService;
			this.packInfoUI = packInfoUI;
			this.packProvider = packProvider;
			this.resetService = resetService;
		}

		public override void Enter()
		{
			base.Enter();
			EnterAsync();
		}

		private async void EnterAsync()
		{
			resetService.Reset();
			PlaySceneTransition();

			await levelService.SetupLevelAsync();

			healthController.ResetHealth();
			SetupUi();

			StateMachine.ChangeState<ResetState>();
		}

		private void PlaySceneTransition()
		{
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
