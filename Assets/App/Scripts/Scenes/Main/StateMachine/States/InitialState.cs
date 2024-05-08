using Features.StateMachine.States;
using UnityEngine;

namespace Scenes.Main.StateMachine.States
{
	public class InitialState : State
	{
		public override void Enter()
		{
			base.Enter();
			Debug.Log("Initial State");
		}
	}
}
