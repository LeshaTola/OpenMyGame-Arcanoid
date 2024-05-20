using Features.StateMachine;
using Features.StateMachine.States;
using Scenes.Gameplay.Feature.Field;
using Scenes.Gameplay.Feature.Health;
using Scenes.Gameplay.Feature.Progress;
using Scenes.Gameplay.StateMachine.States.Loss;
using Scenes.Gameplay.StateMachine.States.Win;
using System.Collections.Generic;

namespace Scenes.Gameplay.StateMachine.States
{
	public class GameplayState : State
	{
		private IProgressController progressController;
		private IHealthController healthController;
		private IBoundaryValidator boundaryValidator;

		private List<IUpdatable> updatables;

		public GameplayState(IProgressController progressController,
					   IHealthController healthController,
					   IBoundaryValidator boundaryValidator,
					   List<IUpdatable> updatables)
		{
			this.progressController = progressController;
			this.healthController = healthController;
			this.boundaryValidator = boundaryValidator;
			this.updatables = updatables;
		}

		public override void Enter()
		{
			base.Enter();
			progressController.OnWin += OnWin;
			boundaryValidator.OnLastBallFall += OnLastBallFall;
			healthController.OnDeath += OnDeath;
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
			progressController.OnWin -= OnWin;
			boundaryValidator.OnLastBallFall -= OnLastBallFall;
			healthController.OnDeath -= OnDeath;
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
