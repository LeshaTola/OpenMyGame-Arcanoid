using Features.StateMachine.States;
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

		private void SetState(Type type)
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

		public void SetState<T>() where T : State
		{
			var type = typeof(T);
			SetState(type);
		}

		public void Update()
		{
			currentState?.Update();
		}
	}
}