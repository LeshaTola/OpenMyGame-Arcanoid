using Features.StateMachine.States.General;
using Scenes.Gameplay.Feature.LevelCreation.Mechanics.Controllers;
using Scenes.Gameplay.Feature.LevelCreation.Saves;

namespace Scenes.Gameplay.StateMachine.States.LoadScene
{
	public class CleanupLoadSceneStateStep : StateStep
	{
		private ILevelMechanicsController levelMechanicsController;
		private ILevelSavingService levelSavingService;

		public CleanupLoadSceneStateStep(ILevelMechanicsController levelMechanicsController,
								   ILevelSavingService levelSavingService)
		{
			this.levelMechanicsController = levelMechanicsController;
			this.levelSavingService = levelSavingService;
		}

		public override void Enter()
		{
			base.Enter();
			levelMechanicsController.Cleanup();
			levelSavingService.Cleanup();
		}


	}
}
