using Features.StateMachine.States;
using Features.StateMachine.States.General;
using System;
using System.Collections.Generic;

namespace Features.StateMachine
{
	public class StateMachine
	{
		private State currentState;
		private Dictionary<Type, State> states = new();

		public void AddState(State state)
		{
			states.Add(state.GetType(), state);
		}

		public void ChangeState(Type type)
		{
			if (currentState != null && currentState.GetType() == type)
			{
				return;
			}

			if (states.ContainsKey(type))
			{
				currentState?.Exit();

				currentState = states[type];

				currentState.Enter();
			}
		}

		public void ChangeState<T>() where T : State
		{
			var type = typeof(T);
			ChangeState(type);
		}

		public void Update()
		{
			currentState?.Update();
		}

		public void AddStep<T>(IStateStep stateStep) where T : State
		{
			var type = typeof(T);
			if (states.ContainsKey(type))
			{
				states[type]?.AddStep(stateStep);
			}
		}
	}
}