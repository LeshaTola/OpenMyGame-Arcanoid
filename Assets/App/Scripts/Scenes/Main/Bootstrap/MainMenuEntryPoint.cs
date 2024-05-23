using Features.StateMachine;
using Features.StateMachine.States;
using System.Collections.Generic;
using TNRD;
using UnityEngine;

namespace Scenes.Main.Bootstrap
{
	public class MainMenuEntryPoint : MonoBehaviour
	{
		[SerializeField] List<SerializableInterface<Features.Bootstrap.IInitializable>> initializables;
		[SerializeField] MainMenuStateMachineHandler stateMachineHandler;

		public void Start()
		{
			foreach (var initializable in initializables)
			{
				initializable.Value.Init();
			}

			InitStateMachine();
		}

		private void InitStateMachine()
		{
			stateMachineHandler.Init();

			stateMachineHandler.StartStateMachine<GlobalInitialState>();
		}
	}
}