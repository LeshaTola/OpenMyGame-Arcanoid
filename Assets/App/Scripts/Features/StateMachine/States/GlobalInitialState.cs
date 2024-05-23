using Features.ProjectInitServices;
using System;

namespace Features.StateMachine.States
{
	public class GlobalInitialState : State
	{
		private Type nextState;
		private IProjectInitService projectInitService;

		public GlobalInitialState(IProjectInitService projectInitService)
		{
			this.projectInitService = projectInitService;
		}

		public Type NextState { get => nextState; set => nextState = value; }

		public override void Enter()
		{
			projectInitService.InitProject();
			StateMachine.ChangeState(nextState);
		}
	}
}
