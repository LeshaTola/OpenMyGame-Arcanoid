using Features.StateMachine.Factories;
using Features.StateMachine.States;
using SceneReference;
using Scenes.Main.StateMachine.States.Initial;
using Scenes.Main.StateMachine.States.LoadGameplayScene;
using UnityEngine;
using Zenject;

namespace Features.StateMachine
{
	public class MainMenuStateMachineHandler : MonoBehaviour
	{
		[SerializeField] private SceneRef nextScene;
		[SerializeField] private SceneRef gameplayScene;

		private StateMachine core;
		private IStatesFactory statesFactory;
		private IStateStepsFactory stateStepsFactory;

		[Inject]
		public void Construct(StateMachine core, IStatesFactory statesFactory, IStateStepsFactory stateStepsFactory)
		{
			this.core = core;
			this.statesFactory = statesFactory;
			this.stateStepsFactory = stateStepsFactory;
		}

		public void Init()
		{
			var globalInitState = statesFactory.GetState<GlobalInitialState>();
			globalInitState.NextState = typeof(MainMenuInitialState);
			core.AddState(globalInitState);
			core.AddState(statesFactory.GetState<MainMenuInitialState>());
			SetupLoadSceneState();
			SetupLoadGameplaySceneState();
		}

		private void SetupLoadGameplaySceneState()
		{
			core.AddState(statesFactory.GetState<LoadGameplaySceneState>());
			var loadMainMenuSceneState = stateStepsFactory.GetStateStep<LoadSceneStateStep>();
			loadMainMenuSceneState.Scene = gameplayScene;
			core.AddStep<LoadGameplaySceneState>(loadMainMenuSceneState);
		}

		private void SetupLoadSceneState()
		{
			core.AddState(statesFactory.GetState<LoadSceneState>());

			var loadNextSceneStep = stateStepsFactory.GetStateStep<LoadSceneStateStep>();
			loadNextSceneStep.Scene = nextScene;
			core.AddStep<LoadSceneState>(loadNextSceneStep);
		}

		public void StartStateMachine<T>() where T : State
		{
			core.ChangeState<T>();
		}

		private void Update()
		{
			core.Update();
		}
	}
}