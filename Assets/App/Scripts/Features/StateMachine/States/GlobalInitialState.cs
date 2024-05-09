using UnityEngine;

namespace App.Scripts.Features.StateMachine.States
{
	public class GlobalInitialState : State
	{
		[SerializeField] private int targetFrameRate = 60;
		[SerializeField] private State nextState;

		private static bool isValid = true;

		public override void Enter()
		{
			base.Enter();
			if (isValid)
			{
				isValid = false;
				Application.targetFrameRate = targetFrameRate;
			}
			stateMachine.ChangeState(nextState.GetType());
		}
	}
}
