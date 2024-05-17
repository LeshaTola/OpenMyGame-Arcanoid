using Features.StateMachine;
using Features.StateMachine.States;
using Scenes.Gameplay.StateMachine.States.Win;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Bootstrap
{
	public class StateMachineInstaller : MonoInstaller
	{
		[SerializeField] StateMachineHandler stateMachineHandler;

		public override void Start()
		{
			stateMachineHandler.Init();
			stateMachineHandler.Core.AddStep<WinState>(Container.Instantiate<WinStateStep>());
			stateMachineHandler.StartStateMachine<GlobalInitialState>();
		}

		public override void InstallBindings()
		{

		}
	}
}
