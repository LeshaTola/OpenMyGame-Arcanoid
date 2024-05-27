using Features.StateMachine.States;
using Features.UI.SceneTransitions;
using Scenes.Main.Feature.UI;
using Scenes.Main.StateMachine.States.InitialState.Routers;

namespace Scenes.Main.StateMachine.States.Initial
{
	public class MainMenuInitialState : State
	{
		private ISceneTransition sceneTransition;
		private IRouterShowLanguages routerShowLanguages;
		private MainMenuUI mainMenuUI;

		public MainMenuInitialState(ISceneTransition sceneTransition,
							  MainMenuUI mainMenuUI,
							  IRouterShowLanguages routerShowLanguages)
		{
			this.sceneTransition = sceneTransition;
			this.mainMenuUI = mainMenuUI;
			this.routerShowLanguages = routerShowLanguages;
		}

		public override void Enter()
		{
			base.Enter();
			sceneTransition.PlayOff();
			mainMenuUI.OnPlayButtonClicked += OnPlayButtonPressed;
			mainMenuUI.OnSwapLanguageButtonClicked += OnSwapLanguageButtonClicked;
		}

		public override void Exit()
		{
			mainMenuUI.OnPlayButtonClicked -= OnPlayButtonPressed;
			mainMenuUI.OnSwapLanguageButtonClicked -= OnSwapLanguageButtonClicked;
		}

		private void OnPlayButtonPressed()
		{
			StateMachine.ChangeState<LoadSceneState>();
		}

		private void OnSwapLanguageButtonClicked()
		{
			routerShowLanguages.ShowLanguages();
		}
	}
}
