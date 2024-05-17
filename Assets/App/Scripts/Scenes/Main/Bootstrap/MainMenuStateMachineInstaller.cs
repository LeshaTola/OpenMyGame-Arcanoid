using Features.StateMachine;
using Features.StateMachine.States;
using Features.UI.SceneTransitions;
using SceneReference;
using System.Collections.Generic;
using TNRD;
using UnityEngine;
using Zenject;

namespace Scenes.Main.Bootstrap
{
	public class MainMenuStateMachineInstaller : MonoInstaller
	{
		[SerializeField] StateMachineHandler stateMachineHandler;
		[SerializeField] private SceneRef scene;
		[SerializeField] private SerializableInterface<ISceneTransition> sceneTransition;

		public override void Start()
		{
			stateMachineHandler.Init();


			SetupLoadSceneState();

			stateMachineHandler.StartStateMachine<GlobalInitialState>();
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

		}
	}
}