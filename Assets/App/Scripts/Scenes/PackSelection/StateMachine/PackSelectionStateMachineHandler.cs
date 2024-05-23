using Features.StateMachine.Factories;
using Features.StateMachine.States;
using SceneReference;
using UnityEngine;
using Zenject;

namespace Scenes.PackSelection.StateMachine
{
	public class PackSelectionStateMachineHandler : MonoBehaviour
	{
		[SerializeField] private SceneRef gameplayScene;
		[SerializeField] private SceneRef mainMenuScene;

		private Features.StateMachine.StateMachine core;
		private IStatesFactory statesFactory;
		private IStateStepsFactory stateStepsFactory;

		[Inject]
		public void Construct(Features.StateMachine.StateMachine core, IStatesFactory statesFactory, IStateStepsFactory stateStepsFactory)
		{
			this.core = core;
			this.statesFactory = statesFactory;
			this.stateStepsFactory = stateStepsFactory;
		}

		public void Init()
		{
			var globalInitState = statesFactory.GetState<GlobalInitialState>();
			globalInitState.NextState = typeof(PackSelectionInitState);
			core.AddState(globalInitState);

			core.AddState(statesFactory.GetState<PackSelectionInitState>());

			SetupLoadSceneState();
			SetupLoadMainMenuState();
		}

		private void SetupLoadMainMenuState()
		{
			core.AddState(statesFactory.GetState<LoadMainMenuState>());
			var loadMainMenuSceneState = stateStepsFactory.GetStateStep<LoadSceneStateStep>();
			loadMainMenuSceneState.Scene = mainMenuScene;
			core.AddStep<LoadMainMenuState>(stateStepsFactory.GetStateStep<LoadSceneStateStep>());
		}

		private void SetupLoadSceneState()
		{
			core.AddState(statesFactory.GetState<LoadSceneState>());
			var loadGameplaySceneState = stateStepsFactory.GetStateStep<LoadSceneStateStep>();
			loadGameplaySceneState.Scene = gameplayScene;
			core.AddStep<LoadSceneState>(loadGameplaySceneState);
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
