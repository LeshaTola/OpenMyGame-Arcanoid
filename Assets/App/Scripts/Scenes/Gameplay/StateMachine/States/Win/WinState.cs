using Features.StateMachine.States;
using Scenes.Gameplay.StateMachine.States.Win.Routers;

namespace Scenes.Gameplay.StateMachine.States.Win
{
	public class WinState : State
	{
		private IRouterShowWin routerShowWin;

		public WinState(IRouterShowWin routerShowWin)
		{
			this.routerShowWin = routerShowWin;
		}

		public override void Enter()
		{
			base.Enter();
			routerShowWin.ShowWin();
		}

		public override void Exit()
		{
			base.Exit();
		}
	}
}
