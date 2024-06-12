using Features.Bootstrap;
using Features.StateMachine;
using Features.StateMachine.States;
using Scenes.Gameplay.StateMachine.States;
using Scenes.Gameplay.StateMachine.States.LoadScene;
using Scenes.Gameplay.StateMachine.States.Loss;
using Scenes.Gameplay.StateMachine.States.Win;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Bootstrap
{
	public class GameplayStatesInstaller : MonoInstaller
	{
		[SerializeField] private GameplayStateMachineHandler stateMachineHandler;

		public override void InstallBindings()
		{
			StatesFactoriesInstaller.Install(Container);

			BindStateMachine();
			BindResetState();
			BindGameplayState();
			BindPauseState();
			BindLossState();
			BindWinState();
			BindInitialState();
			BindLoadNextState();
		}

		private void BindGameplayState()
		{
			Container.Bind<GameplayState>().AsSingle();
		}

		private void BindWinState()
		{
			Container.Bind<WinState>().AsSingle();
		}

		private void BindLossState()
		{
			Container.Bind<LossState>().AsSingle();
		}

		private void BindInitialState()
		{
			Container.Bind<InitialState>().AsSingle();
		}

		private void BindPauseState()
		{
			Container.Bind<PauseState>().AsSingle();
		}

		private void BindResetState()
		{
			Container.Bind<ResetState>().AsSingle();
		}

		private void BindLoadNextState()
		{
			Container.Bind<LoadSceneState>().AsSingle();
			Container.Bind<LoadSceneStateStep>().AsSingle();
			Container.Bind<CleanupLoadSceneStateStep>().AsSingle();
		}

		private void BindStateMachine()
		{
			Container.Bind<Features.StateMachine.StateMachine>().AsSingle();
			Container.BindInstance(stateMachineHandler).AsSingle();
		}
	}
}
