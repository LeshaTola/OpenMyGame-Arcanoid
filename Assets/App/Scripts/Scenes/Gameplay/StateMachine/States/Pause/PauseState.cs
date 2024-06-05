using Features.StateMachine.States;
using Module.TimeProvider;
using Scenes.Gameplay.Feature.Player.Ball.Services;
using Scenes.Gameplay.StateMachine.States.Win.Routers;

namespace Scenes.Gameplay.StateMachine.States
{
	public class PauseState : State
	{

		private IRouterShowMenu routerShowMenu;
		private ITimeProvider timeProvider;
		private IBallService ballService;

		public PauseState(IRouterShowMenu routerShowMenu,
					ITimeProvider timeProvider,
					IBallService ballService)
		{
			this.routerShowMenu = routerShowMenu;
			this.timeProvider = timeProvider;
			this.ballService = ballService;
		}

		public override void Enter()
		{
			base.Enter();
			timeProvider.TimeMultiplier = 0;
			routerShowMenu.ShowMenu();
			ballService.StopBalls();
		}

		public override void Exit()
		{
			base.Exit();
			timeProvider.TimeMultiplier = 1;
			ballService.PushBalls();
		}
	}
}
