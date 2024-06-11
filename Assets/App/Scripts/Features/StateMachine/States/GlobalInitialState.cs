using Features.ProjectInitServices;
using System;
using UnityEngine;

namespace Features.StateMachine.States
{
	public class GlobalInitialState : State
	{
		private Type nextState;
		private IProjectInitService projectInitService;

		private static bool isValid = true;

		public GlobalInitialState(IProjectInitService projectInitService)
		{
			this.projectInitService = projectInitService;
			Debug.Log("GlobalInitState");
		}

		public Type NextState { get => nextState; set => nextState = value; }

		public override void Enter()
		{
			if (isValid)
			{
				projectInitService.InitProject();

				isValid = false;
			}

			StateMachine.ChangeState(nextState);
		}
	}
}
