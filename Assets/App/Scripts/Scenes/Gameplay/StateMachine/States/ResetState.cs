using Features.StateMachine.States;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.StateMachine.States
{
	public class ResetState : State
	{
		[SerializeField] private List<IResetable> resetables = new();

		public override void Enter()
		{
			base.Enter();
			foreach (IResetable resetable in resetables)
			{
				resetable.Reset();
			}
			StateMachine.ChangeState<GameplayState>();
		}
	}
}
