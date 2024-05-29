using Features.StateMachine;
using Features.StateMachine.States;
using Scenes.Gameplay.Feature.Bonuses.Services;
using Scenes.Gameplay.Feature.Field;
using Scenes.Gameplay.Feature.Health;
using Scenes.Gameplay.Feature.Player;
using Scenes.Gameplay.Feature.Player.PlayerInput;
using Scenes.Gameplay.Feature.Progress;
using Scenes.Gameplay.Feature.UI;
using Scenes.Gameplay.StateMachine.States.Loss;
using Scenes.Gameplay.StateMachine.States.Win;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.StateMachine.States
{
	public class GameplayState : State
	{
		private IProgressController progressController;
		private IHealthController healthController;
		private IBoundaryValidator boundaryValidator;
		private IBonusService bonusService;
		private IInput input;
		private IFieldSizeProvider fieldSizeProvider;
		private Plate plate;
		private GameplayHeaderUI headerUI;

		private List<IUpdatable> updatables;

		public GameplayState(IProgressController progressController,
					   IHealthController healthController,
					   IBoundaryValidator boundaryValidator,
					   IInput input,
					   IFieldSizeProvider fieldSizeProvider,
					   IBonusService bonusService,
					   Plate plate,
					   GameplayHeaderUI headerUI,
					   List<IUpdatable> updatables)
		{
			this.input = input;
			this.fieldSizeProvider = fieldSizeProvider;
			this.plate = plate;
			this.headerUI = headerUI;
			this.progressController = progressController;
			this.healthController = healthController;
			this.boundaryValidator = boundaryValidator;
			this.bonusService = bonusService;
			this.updatables = updatables;
		}

		public override void Enter()
		{
			base.Enter();
			headerUI.OnMenuButtonCLicked += OnMenuButtonCLicked;
			headerUI.OnNextLevelButtonCLicked += OnNextLevelButtonCLicked;

			progressController.OnWin += OnWin;
			boundaryValidator.OnLastBallFall += OnLastBallFall;
			healthController.OnDeath += OnDeath;
			input.OnEndInput += OnEndInput;
		}

		public override void Update()
		{
			base.Update();
			foreach (var updatable in updatables)
			{
				updatable.Update();
			}
			bonusService.UpdateBonus();
		}

		public override void Exit()
		{
			base.Exit();
			headerUI.OnMenuButtonCLicked -= OnMenuButtonCLicked;
			headerUI.OnNextLevelButtonCLicked -= OnNextLevelButtonCLicked;
			progressController.OnWin -= OnWin;
			boundaryValidator.OnLastBallFall -= OnLastBallFall;
			healthController.OnDeath -= OnDeath;
			input.OnEndInput -= OnEndInput;
		}

		private void OnDeath()
		{
			StateMachine.ChangeState<LossState>();
		}

		private void OnLastBallFall()
		{
			healthController.ReduceHealth(1);
			if (healthController.CurrentHealth > 0)
			{
				StateMachine.ChangeState<ResetState>();
			}
		}

		private void OnWin()
		{
			StateMachine.ChangeState<WinState>();
		}

		private void OnEndInput(Vector2 lastPosition)
		{
			if (!fieldSizeProvider.GameField.IsValid(lastPosition))
			{
				return;
			}
			plate.PushBalls();
		}

		private void OnMenuButtonCLicked()
		{
			StateMachine.ChangeState<PauseState>();
		}

		private void OnNextLevelButtonCLicked()
		{
			progressController.InitiateWin();
		}
	}
}
