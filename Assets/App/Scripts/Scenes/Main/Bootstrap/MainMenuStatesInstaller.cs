using Features.Bootstrap;
using Features.StateMachine;
using Features.StateMachine.States;
using Scenes.Main.StateMachine.States.Initial;
using Scenes.Main.StateMachine.States.LoadGameplayScene;
using UnityEngine;
using Zenject;

namespace Scenes.Main.Bootstrap
{
	public class MainMenuStatesInstaller : MonoInstaller
	{
		[SerializeField] MainMenuStateMachineHandler stateMachineHandler;

		public override void InstallBindings()
		{
			StatesFactoriesInstaller.Install(Container);
			MainMenuRoutersInstaller.Install(Container);

			BindStateMachine();
			BindInitialState();
			BindLoadNextState();
		}

		private void BindInitialState()
		{
			Container.Bind<MainMenuInitialState>().AsSingle();
		}

		private void BindLoadNextState()
		{
			Container.Bind<LoadGameplaySceneState>().AsSingle();
			Container.Bind<LoadSceneState>().AsSingle();
			Container.Bind<LoadSceneStateStep>().AsTransient();
		}

		private void BindStateMachine()
		{
			Container.Bind<Features.StateMachine.StateMachine>().AsSingle();
			Container.BindInstance(stateMachineHandler).AsSingle();
		}
	}
}