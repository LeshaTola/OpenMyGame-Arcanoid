using Features.StateMachine.States;
using Scenes.PackSelection.StateMachine;
using System.Collections.Generic;
using TNRD;
using UnityEngine;

namespace Scenes.PackSelection.Bootstrap
{
	public class PackSelectionEntryPoint : MonoBehaviour
	{
		[SerializeField] List<SerializableInterface<Features.Bootstrap.IInitializable>> initializables;

		[SerializeField] private PackSelectionStateMachineHandler stateMachineHandler;

		public void Start()
		{
			foreach (var initializable in initializables)
			{
				initializable.Value.Init();
				InitStateMachine();
			}
		}

		private void InitStateMachine()
		{
			stateMachineHandler.Init();

			stateMachineHandler.StartStateMachine<GlobalInitialState>();
		}
	}
}
