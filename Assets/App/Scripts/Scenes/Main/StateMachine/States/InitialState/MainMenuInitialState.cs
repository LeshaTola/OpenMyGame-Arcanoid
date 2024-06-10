using Features.Saves.Gameplay.Providers;
using Features.StateMachine.States;
using Features.UI.SceneTransitions;
using Scenes.Main.Feature.UI;
using Scenes.Main.StateMachine.States.InitialState.Routers;
using Scenes.Main.StateMachine.States.LoadGameplayScene;

namespace Scenes.Main.StateMachine.States.Initial
{
	public class MainMenuInitialState : State
	{
		private ISceneTransition sceneTransition;
		private IRouterShowLanguages routerShowLanguages;
		private MainMenuUI mainMenuUI;
		private IGameplaySavesProvider gameplaySavesProvider;

		public MainMenuInitialState(ISceneTransition sceneTransition,
							  MainMenuUI mainMenuUI,
							  IRouterShowLanguages routerShowLanguages,
							  IGameplaySavesProvider gameplaySavesProvider)
		{
			this.sceneTransition = sceneTransition;
			this.mainMenuUI = mainMenuUI;
			this.routerShowLanguages = routerShowLanguages;
			this.gameplaySavesProvider = gameplaySavesProvider;
		}

		public override void Enter()
		{
			base.Enter();
			mainMenuUI.UpdateUI(gameplaySavesProvider.CanContinue());

			sceneTransition.PlayOff();
			mainMenuUI.OnPlayButtonClicked += OnPlayButtonPressed;
			mainMenuUI.OnSwapLanguageButtonClicked += OnSwapLanguageButtonClicked;
			mainMenuUI.OnContinueButtonClicked += OnContinueButtonClicked;
		}

		public override void Exit()
		{
			mainMenuUI.OnPlayButtonClicked -= OnPlayButtonPressed;
			mainMenuUI.OnSwapLanguageButtonClicked -= OnSwapLanguageButtonClicked;
			mainMenuUI.OnContinueButtonClicked -= OnContinueButtonClicked;
		}

		private void OnPlayButtonPressed()
		{
			StateMachine.ChangeState<LoadSceneState>();
		}

		private void OnSwapLanguageButtonClicked()
		{
			routerShowLanguages.ShowLanguages();
		}

		private void OnContinueButtonClicked()
		{
			gameplaySavesProvider.IsContinue = true;
			StateMachine.ChangeState<LoadGameplaySceneState>();
		}

	}
}
