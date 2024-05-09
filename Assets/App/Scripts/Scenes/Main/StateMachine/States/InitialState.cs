using App.Scripts.Features.StateMachine.States;
using UnityEngine;

namespace App.Scripts.Scenes.Main.StateMachine.States
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
