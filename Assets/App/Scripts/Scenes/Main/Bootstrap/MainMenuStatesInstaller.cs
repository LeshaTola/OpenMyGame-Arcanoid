using Features.Bootstrap;
using Features.StateMachine;
using Features.StateMachine.States;
using SceneReference;
using Scenes.Main.StateMachine.States;
using UnityEngine;
using Zenject;

namespace Scenes.Main.Bootstrap
{
	public class MainMenuStatesInstaller : MonoInstaller
	{
		[SerializeField] MainMenuStateMachineHandler stateMachineHandler;
		[SerializeField] private SceneRef nextScene;

		public override void InstallBindings()
		{
			StatesFactoriesInstaller.Install(Container);

			BindStateMachine();
			BindGlobalInitState();
			BindInitialState();
			BindLoadNextState();
		}

		private ConcreteIdArgConditionCopyNonLazyBinder BindInitialState()
		{
			return Container.Bind<MainMenuInitialState>().AsSingle();
		}

		private void BindLoadNextState()
		{
			Container.Bind<LoadSceneState>().AsSingle();
			Container.Bind<LoadSceneStateStep>().AsSingle().WithArguments(nextScene);
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