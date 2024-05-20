using Features.StateMachine;
using Features.StateMachine.States;
using SceneReference;
using Scenes.PackSelection.StateMachine;
using System.Collections.Generic;
using TNRD;
using UnityEngine;
using Zenject;

namespace Scenes.PackSelection.Bootstrap
{
	public class PackSelectionEntryPoint : MonoInstaller
	{
		[SerializeField] List<SerializableInterface<Features.Bootstrap.IInitializable>> initializables;

		[SerializeField] private StateMachineHandler stateMachineHandler;
		[SerializeField] private SceneRef scene;
		[SerializeField] private SceneRef mainMenuScene;

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
			AddLoadSceneState();
			AddLoadMainMenuSceneState();

			stateMachineHandler.StartStateMachine<GlobalInitialState>();
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

		private void AddLoadMainMenuSceneState()
		{
			stateMachineHandler.Core.AddStep<LoadMainMenuState>(
					Container.Instantiate<LoadSceneStateStep>(
					new List<object>
					{
						mainMenuScene,
					}));
		}

		private void AddLoadSceneState()
		{
			stateMachineHandler.Core.AddStep<LoadSceneState>(
					Container.Instantiate<LoadSceneStateStep>(
					new List<object>
					{
						scene,
					}));
		}

		public override void InstallBindings()
		{
			Container.BindInstance(stateMachineHandler);
		}
	}
}
