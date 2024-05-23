using Features.StateMachine.Factories;
using Features.StateMachine.States;
using SceneReference;
using Scenes.Gameplay.StateMachine.States;
using Scenes.Gameplay.StateMachine.States.Loss;
using Scenes.Gameplay.StateMachine.States.Win;
using UnityEngine;
using Zenject;

namespace Features.StateMachine
{
	public class GameplayStateMachineHandler : MonoBehaviour
	{
		[SerializeField] private SceneRef nextScene;

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
			SetupGlobalInitState();

			core.AddState(statesFactory.GetState<InitialState>());
			core.AddState(statesFactory.GetState<ResetState>());
			core.AddState(statesFactory.GetState<GameplayState>());
			core.AddState(statesFactory.GetState<PauseState>());
			core.AddState(statesFactory.GetState<LossState>());
			core.AddState(statesFactory.GetState<WinState>());

			SetupLoadSceneState();
		}

		public void StartStateMachine<T>() where T : State
		{
			core.ChangeState<T>();
		}

		private void Update()
		{
			core.Update();
		}

		private void SetupGlobalInitState()
		{
			var globalInitState = statesFactory.GetState<GlobalInitialState>();
			globalInitState.NextState = typeof(InitialState);
			core.AddState(globalInitState);
		}

		private void SetupLoadSceneState()
		{
			core.AddState(statesFactory.GetState<LoadSceneState>());

			var loadNextSceneStep = stateStepsFactory.GetStateStep<LoadSceneStateStep>();
			loadNextSceneStep.Scene = nextScene;
			core.AddStep<LoadSceneState>(loadNextSceneStep);
		}
	}
}