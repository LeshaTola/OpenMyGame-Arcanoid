using Features.Bootstrap;
using Features.StateMachine;
using Features.StateMachine.States;
using Scenes.Gameplay.Feature.Player;
using Scenes.Gameplay.StateMachine.States;
using Scenes.Gameplay.StateMachine.States.Loss;
using Scenes.Gameplay.StateMachine.States.Win;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Bootstrap
{
	public class GameplayStatesInstaller : MonoInstaller
	{
		[SerializeField] private GameplayStateMachineHandler stateMachineHandler;
		[SerializeField] private TextAsset defaultLevelInfo;
		[SerializeField] private Plate plate;

		public override void InstallBindings()
		{
			StatesFactoriesInstaller.Install(Container);

			BindStateMachine();
			BindGlobalInitState();
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
			var updatables = new List<IUpdatable>
			{
				plate,
			};
			Container.Bind<GameplayState>().AsSingle().WithArguments(updatables);
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
			Container.Bind<InitialState>().AsSingle().WithArguments(defaultLevelInfo);
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
