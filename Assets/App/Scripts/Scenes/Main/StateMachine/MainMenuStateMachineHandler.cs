using Features.StateMachine.Factories;
using Features.StateMachine.States;
using Scenes.Main.StateMachine.States;
using UnityEngine;
using Zenject;

namespace Features.StateMachine
{
	public class MainMenuStateMachineHandler : MonoBehaviour
	{
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
			core.AddState(statesFactory.GetState<LoadSceneState>());

			core.AddStep<LoadSceneState>(stateStepsFactory.GetStateStep<LoadSceneStateStep>());
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