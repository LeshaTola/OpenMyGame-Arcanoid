using Features.StateMachine;
using Features.StateMachine.States;
using Scenes.Gameplay.Feature.Field;
using Scenes.Gameplay.Feature.Health;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.StateMachine.States
{
	public class GameplayState : State
	{
		[SerializeField] HealthController healthController;
		[SerializeField] BoundaryValidator boundaryValidator;
		[SerializeField] private List<IUpdatable> updatables;

		public override void Enter()
		{
			base.Enter();
			healthController.OnDeath += OnDeath;
			boundaryValidator.OnLastBallFall += OnLastBallFall;
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
		}

		private void OnDeath()
		{
			StateMachine.ChangeState<LoadSceneState>();
		}

		private void OnLastBallFall()
		{
			healthController.ReduceHealth(1);
			StateMachine.ChangeState<ResetState>();
		}
	}
}
