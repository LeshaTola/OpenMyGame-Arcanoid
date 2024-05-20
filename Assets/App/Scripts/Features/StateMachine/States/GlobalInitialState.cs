using Features.ProjectInitServices;

namespace Features.StateMachine.States
{
	public class GlobalInitialState : State
	{
		private State nextState;
		private IProjectInitService projectInitService;

		public GlobalInitialState(State nextState, IProjectInitService projectInitService)
		{
			this.nextState = nextState;
			this.projectInitService = projectInitService;
		}

		public override void Enter()
		{
			projectInitService.InitProject();
			StateMachine.ChangeState(nextState.GetType());
		}
	}
}
