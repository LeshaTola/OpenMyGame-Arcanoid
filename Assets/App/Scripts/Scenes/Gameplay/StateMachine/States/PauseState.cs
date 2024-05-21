using Features.StateMachine.States;
using Module.TimeProvider;
using Scenes.Gameplay.StateMachine.States.Win.Routers;

namespace Scenes.Gameplay.StateMachine.States
{
	public class PauseState : State
	{

		private IRouterShowMenu routerShowMenu;
		private ITimeProvider timeProvider;

		public PauseState(IRouterShowMenu routerShowMenu, ITimeProvider timeProvider)
		{
			this.routerShowMenu = routerShowMenu;
			this.timeProvider = timeProvider;
		}

		public override void Enter()
		{
			base.Enter();
			timeProvider.TimeMultiplier = 0;
			routerShowMenu.ShowMenu();
		}

		public override void Exit()
		{
			base.Exit();
			timeProvider.TimeMultiplier = 1;
		}
	}
}
