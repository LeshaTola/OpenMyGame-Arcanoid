using Features.StateMachine.States;

namespace Scenes.Gameplay.StateMachine.States.Loss
{
	public class LossState : State
	{
		IRouterShowLoss routerShowLoss;

		public LossState(IRouterShowLoss routerShowLoss)
		{
			this.routerShowLoss = routerShowLoss;
		}

		public override void Enter()
		{
			base.Enter();

			routerShowLoss.ShowLoss();
		}

		public override void Exit()
		{
			base.Exit();
		}
	}
}
