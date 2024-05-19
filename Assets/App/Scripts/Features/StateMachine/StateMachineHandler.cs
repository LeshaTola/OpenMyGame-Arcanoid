using Features.StateMachine.States;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Features.StateMachine
{
	public class StateMachineHandler : SerializedMonoBehaviour
	{
		[SerializeField] private State startState;
		[SerializeField] private List<State> states = new();

		private StateMachine core;

		public StateMachine Core { get => core; }
		public State StartState { get => startState; }

		public void Init()
		{
			core = new();

			foreach (var state in states)
			{
				state.Init(core);
				core.AddState(state);
			}

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