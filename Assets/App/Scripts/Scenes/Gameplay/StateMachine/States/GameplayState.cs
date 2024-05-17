using Features.StateMachine;
using Features.StateMachine.States;
using Scenes.Gameplay.Feature.Field;
using Scenes.Gameplay.Feature.Health;
using Scenes.Gameplay.Feature.Progress;
using Scenes.Gameplay.StateMachine.States.Loss;
using Scenes.Gameplay.StateMachine.States.Win;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.StateMachine.States
{
	public class GameplayState : State
	{
		[SerializeField] private HealthController healthController;
		[SerializeField] private BoundaryValidator boundaryValidator;
		[SerializeField] private List<IUpdatable> updatables;
		[SerializeField] private IProgressController progressController;

		public override void Enter()
		{
			base.Enter();
			healthController.OnDeath += OnDeath;
			boundaryValidator.OnLastBallFall += OnLastBallFall;
			progressController.OnWin += OnWin;
		}

		public override void Update()
		{
			base.Update();
			foreach (var updatable in updatables)
			{
				updatable.Update();
			}
		}

		public override void Exit()
		{
			base.Exit();
			healthController.OnDeath -= OnDeath;
			boundaryValidator.OnLastBallFall -= OnLastBallFall;
			progressController.OnWin -= OnWin;
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
	}
}
