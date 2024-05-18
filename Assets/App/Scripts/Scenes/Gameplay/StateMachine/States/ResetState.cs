using Features.StateMachine;
using Features.StateMachine.States;
using Features.StateMachine.States.General;
using Scenes.Gameplay.Feature.Field;
using Scenes.Gameplay.Feature.Player;
using Scenes.Gameplay.Feature.Player.PlayerInput;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.StateMachine.States
{
	public class ResetState : State
	{
		[SerializeField] private List<IResetable> resetables = new();
		[SerializeField] private List<IUpdatable> updatables = new();

		public override void Enter()
		{
			base.Enter();
			foreach (IResetable resetable in resetables)
			{
				resetable.Reset();
			}
		}

		public override void Update()
		{
			base.Update();
			foreach (IUpdatable updatable in updatables)
			{
				updatable.Update();
			}
		}
	}

	public class ResetStateStep : StateStep
	{
		private IInput input;
		private IFieldSizeProvider fieldSizeProvider;
		private Plate plate;

		public ResetStateStep(IInput input, IFieldSizeProvider fieldSizeProvider, Plate plate)
		{
			this.input = input;
			this.fieldSizeProvider = fieldSizeProvider;
			this.plate = plate;
		}

		public override void Enter()
		{
			base.Enter();
			input.OnEndInput += OnEndInput;
		}

		public override void Exit()
		{
			base.Exit();
			input.OnEndInput -= OnEndInput;
		}

		private void OnEndInput(Vector2 lastPosition)
		{
			if (!fieldSizeProvider.GameField.IsValid(lastPosition))
			{
				return;
			}
			plate.PushBalls();
			StateMachine.ChangeState<GameplayState>();
		}
	}
}
