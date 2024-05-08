using Features.Bootstrap;
using Features.StateMachine.States;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Features.StateMachine
{
	public class MonoBehStateMachine : SerializedMonoBehaviour, IInitializable
	{
		[SerializeField] private List<State> states = new List<State>();

		private StateMachine core;

		public void Init()
		{
			core = new();

			foreach (var state in states)
			{
				state.Init(core);
				core.AddState(state);
			}
			core.ChangeState(states[0].GetType());
		}

		private void Update()
		{
			core.Update();
		}
	}
}