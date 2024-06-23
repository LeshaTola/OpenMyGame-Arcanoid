using Features.StateMachine.States;
using Scenes.Gameplay.Feature.LevelCreation.Saves;
using Scenes.Gameplay.Feature.Player;
using Scenes.Gameplay.StateMachine.States.Loss.Routers;

namespace Scenes.Gameplay.StateMachine.States.Loss
{
	public class LossState : State
	{
		private IRouterShowLoss routerShowLoss;
		private Plate plate;
		private ILevelSavingService levelSavingService;


		public LossState(IRouterShowLoss routerShowLoss, Plate plate, ILevelSavingService levelSavingService)
		{
			this.routerShowLoss = routerShowLoss;
			this.plate = plate;
			this.levelSavingService = levelSavingService;
		}

		public override void Enter()
		{
			base.Enter();

			plate.Stop();
			levelSavingService.DeleteData();

			routerShowLoss.ShowLoss();
		}
	}
}
