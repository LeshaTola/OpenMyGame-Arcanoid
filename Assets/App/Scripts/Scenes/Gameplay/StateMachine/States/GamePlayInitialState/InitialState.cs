using Features.StateMachine.States;
using Features.UI.SceneTransitions;
using Scenes.Gameplay.Feature.Health;
using UnityEngine;

namespace Scenes.Gameplay.StateMachine.States
{
	public class InitialState : State
	{
		[SerializeField] private HealthController healthController;
		[SerializeField] private ISceneTransition sceneTransition;

		public override void Enter()
		{
			base.Enter();
			healthController.ResetHealth();
			StateMachine.ChangeState<ResetState>();
		}

		public override void Exit()
		{
			base.Exit();
			sceneTransition.PlayOff();
		}

	}
}
