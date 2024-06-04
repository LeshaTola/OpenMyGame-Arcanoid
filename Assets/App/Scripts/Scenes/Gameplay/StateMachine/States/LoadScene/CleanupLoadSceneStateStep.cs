using Features.StateMachine.States.General;
using Scenes.Gameplay.Feature.LevelCreation.Mechanics.Controllers;

namespace Scenes.Gameplay.StateMachine.States.LoadScene
{
	public class CleanupLoadSceneStateStep : StateStep
	{
		private ILevelMechanicsController levelMechanicsController;

		public CleanupLoadSceneStateStep(ILevelMechanicsController levelMechanicsController)
		{
			this.levelMechanicsController = levelMechanicsController;
		}

		public override void Enter()
		{
			base.Enter();
			levelMechanicsController.CleanUp();
		}


	}
}
