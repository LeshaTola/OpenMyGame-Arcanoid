using Features.StateMachine;
using Features.StateMachine.States;
using Features.UI.SceneTransitions;
using SceneReference;
using Scenes.Gameplay.Feature.Player;
using Scenes.Gameplay.Feature.UI;
using Scenes.Gameplay.StateMachine.States;
using Scenes.Gameplay.StateMachine.States.GamePlayInitialState;
using Scenes.Gameplay.StateMachine.States.Loss;
using Scenes.Gameplay.StateMachine.States.Win;
using System.Collections.Generic;
using TNRD;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Bootstrap
{
	public class GameplayEntryPoint : MonoInstaller
	{
		[SerializeField] List<SerializableInterface<Features.Bootstrap.IInitializable>> initializables;

		[SerializeField] StateMachineHandler stateMachineHandler;
		[SerializeField] private SceneRef scene;
		[SerializeField] private SerializableInterface<ISceneTransition> sceneTransition;
		[SerializeField] private Plate plate;

		[SerializeField] private TextAsset defaultLevelInfo;
		[SerializeField] private PackInfoUI packInfoUI;

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
			SetupInitialState();
			stateMachineHandler.Core.AddStep<WinState>(Container.Instantiate<WinStateStep>());
			stateMachineHandler.Core.AddStep<LossState>(Container.Instantiate<LossStateStep>());
			SetupResetState();
			SetupLoadSceneState();

			stateMachineHandler.StartStateMachine<GlobalInitialState>();
		}

		private void SetupInitialState()
		{
			stateMachineHandler.Core.AddStep<InitialState>(Container.Instantiate<GameplayInitialStateStep>(new List<object>()
			{
				defaultLevelInfo,
				packInfoUI
			}));
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

		private void SetupResetState()
		{
			stateMachineHandler.Core.AddStep<ResetState>(Container
				.Instantiate<ResetStateStep>(
				new List<object>
					{
						plate
					}));
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
