using Features.Bootstrap;
using Features.StateMachine.States;
using Scenes.PackSelection.StateMachine;
using UnityEngine;
using Zenject;

namespace Scenes.PackSelection.Bootstrap
{
	public class PackSelectionStatesInstaller : MonoInstaller
	{
		[SerializeField] private PackSelectionStateMachineHandler stateMachineHandler;

		public override void InstallBindings()
		{
			StatesFactoriesInstaller.Install(Container);

			BindStateMachine();
			BindGlobalInitState();
			BindInitialState();
			BindLoadNextState();
		}

		private void BindInitialState()
		{
			Container.Bind<PackSelectionInitState>().AsSingle();
		}

		private void BindLoadNextState()
		{
			Container.Bind<LoadSceneState>().AsSingle();
			Container.Bind<LoadMainMenuState>().AsSingle();
			Container.Bind<LoadSceneStateStep>().AsTransient();
		}

		private void BindGlobalInitState()
		{
			Container.Bind<GlobalInitialState>().AsSingle();
		}

		private void BindStateMachine()
		{
			Container.Bind<Features.StateMachine.StateMachine>().AsSingle();
			Container.BindInstance(stateMachineHandler).AsSingle();
		}
	}
}
