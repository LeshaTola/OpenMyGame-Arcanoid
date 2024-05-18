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
	public class GamePlayEntryPoint : MonoInstaller
	{
		[SerializeField] List<SerializableInterface<Features.Bootstrap.IInitializable>> initializables;

		[SerializeField] StateMachineHandler stateMachineHandler;
		[SerializeField] private SceneRef scene;
		[SerializeField] private SerializableInterface<ISceneTransition> sceneTransition;
		[SerializeField] private Plate plate;

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

			stateMachineHandler.Core.AddStep<WinState>(Container.Instantiate<WinStateStep>());
			stateMachineHandler.Core.AddStep<LossState>(Container.Instantiate<LossStateStep>());
			SetupResetState();
			SetupLoadSceneState();

			stateMachineHandler.StartStateMachine<GlobalInitialState>();
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
