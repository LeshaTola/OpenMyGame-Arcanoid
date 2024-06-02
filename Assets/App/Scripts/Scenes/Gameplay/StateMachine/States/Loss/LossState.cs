using Features.StateMachine.States;
using Scenes.Gameplay.Feature.Player;
using Scenes.Gameplay.StateMachine.States.Loss.Routers;

namespace Scenes.Gameplay.StateMachine.States.Loss
{
	public class LossState : State
	{
		private IRouterShowLoss routerShowLoss;
		private Plate plate;

		public LossState(IRouterShowLoss routerShowLoss, Plate plate)
		{
			this.routerShowLoss = routerShowLoss;
			this.plate = plate;
		}

		public override void Enter()
		{
			base.Enter();
			plate.Stop();

			routerShowLoss.ShowLoss();
		}
	}
}
