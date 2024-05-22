using Features.StateMachine;
using Features.StateMachine.States;
using Features.UI.SceneTransitions;
using SceneReference;
using Scenes.Gameplay.Feature.Player;
using Scenes.Gameplay.StateMachine.States;
using Scenes.Gameplay.StateMachine.States.Loss;
using Scenes.Gameplay.StateMachine.States.Win;
using System.Collections.Generic;
using TNRD;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Bootstrap
{
	public class AAAAA : MonoInstaller
	{
		[SerializeField] List<SerializableInterface<Features.Bootstrap.IInitializable>> initializables;

		[SerializeField] StateMachineHandler stateMachineHandler;
		[SerializeField] private SceneRef scene;
		[SerializeField] private SerializableInterface<ISceneTransition> sceneTransition;
		[SerializeField] private Plate plate;

		[SerializeField] private TextAsset defaultLevelInfo;

		public override void Start()
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

			AddGlobalInitState();
			AddInitialState();
			AddGamePlayState();
			AddPauseState();
			AddWinState();
			AddLossState();
			SetupLoadSceneState();

			stateMachineHandler.StartStateMachine<GlobalInitialState>();
		}

		private void AddGamePlayState()
		{
			GameplayState gameplayState = Container.Instantiate<GameplayState>(new List<object>
			{
				new List<IUpdatable>
				{
					plate
				}
			});
			gameplayState.Init(stateMachineHandler.Core);
			stateMachineHandler.Core.AddState(gameplayState);
		}

		private void AddPauseState()
		{
			PauseState pauseState = Container.Instantiate<PauseState>();
			pauseState.Init(stateMachineHandler.Core);
			stateMachineHandler.Core.AddState(pauseState);
		}

		private void AddLossState()
		{
			LossState lossState = Container.Instantiate<LossState>();
			lossState.Init(stateMachineHandler.Core);
			stateMachineHandler.Core.AddState(lossState);
		}

		private void AddWinState()
		{
			WinState winState = Container.Instantiate<WinState>();
			winState.Init(stateMachineHandler.Core);
			stateMachineHandler.Core.AddState(winState);
		}

		private void AddInitialState()
		{
			var initialState = Container.Instantiate<InitialState>(new List<object>()
			{
				defaultLevelInfo
			});
			initialState.Init(stateMachineHandler.Core);
			stateMachineHandler.Core.AddState(initialState);
		}

		private void AddGlobalInitState()
		{
			var globalInitState = Container.Instantiate<GlobalInitialState>(
			new List<object>
			{
				stateMachineHandler.StartState
			});
			globalInitState.Init(stateMachineHandler.Core);
			stateMachineHandler.Core.AddState(globalInitState);
		}

		private void SetupLoadSceneState()
		{
			stateMachineHandler.Core.AddStep<LoadSceneState>(Container
				.Instantiate<LoadSceneStateStep>(
				new List<object>
					{
						scene,
						sceneTransition.Value
					}));
		}

		public override void InstallBindings()
		{
			Container.BindInstance(stateMachineHandler);
		}
	}
}
