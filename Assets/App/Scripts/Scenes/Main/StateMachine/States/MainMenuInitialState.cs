using Features.StateMachine.States;
using Features.UI.SceneTransitions;
using Scenes.Main.Feature.UI;

namespace Scenes.Main.StateMachine.States
{
	public class MainMenuInitialState : State
	{
		private ISceneTransition sceneTransition;
		private MainMenuUI mainMenuUI;

		public MainMenuInitialState(ISceneTransition sceneTransition, MainMenuUI mainMenuUI)
		{
			this.sceneTransition = sceneTransition;
			this.mainMenuUI = mainMenuUI;
		}

		public override void Enter()
		{
			base.Enter();
			sceneTransition.PlayOff();
			mainMenuUI.OnPlayButtonPressed += OnPlayButtonPressed;
		}

		public override void Exit()
		{
			mainMenuUI.OnPlayButtonPressed -= OnPlayButtonPressed;
		}

		private void OnPlayButtonPressed()
		{
			StateMachine.ChangeState<LoadSceneState>();
		}
	}
}
