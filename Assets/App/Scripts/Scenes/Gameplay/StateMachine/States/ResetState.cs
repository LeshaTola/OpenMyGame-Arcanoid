using Features.StateMachine;
using Features.StateMachine.States;
using Scenes.Gameplay.Feature.Player;
using Scenes.Gameplay.Feature.Player.PlayerInput;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.StateMachine.States
{
	public class ResetState : State
	{
		[SerializeField] private List<IResetable> resetables = new();
		[SerializeField] private Plate plate;
		[SerializeField] private InputController inputController;
		[SerializeField] private List<IUpdatable> updatables = new();

		public override void Enter()
		{
			base.Enter();
			foreach (IResetable resetable in resetables)
			{
				resetable.Reset();
			}
			inputController.Input.OnEndInput += OnEndInput;
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
			inputController.Input.OnEndInput -= OnEndInput;
		}

		private void OnEndInput()
		{
			plate.PushBalls();
			StateMachine.ChangeState<GameplayState>();
		}
	}
}
