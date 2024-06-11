using Features.ProjectInitServices;
using System;

namespace Features.StateMachine.States
{
	public class GlobalInitialState : State
	{
		private Type nextState;
		private IProjectInitService projectInitService;

		private bool isValid = true;

		public GlobalInitialState(IProjectInitService projectInitService)
		{
			this.projectInitService = projectInitService;
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
