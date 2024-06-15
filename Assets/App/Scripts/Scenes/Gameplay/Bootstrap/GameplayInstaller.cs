﻿using Module.TimeProvider;
using Scenes.Gameplay.Feature.Autopilot.Configs;
using Scenes.Gameplay.Feature.Autopilot.Factories;
using Scenes.Gameplay.Feature.Autopilot.Services;
using Scenes.Gameplay.Feature.Blocks.Animation;
using Scenes.Gameplay.Feature.Field;
using Scenes.Gameplay.Feature.Health;
using Scenes.Gameplay.Feature.Health.Configs;
using Scenes.Gameplay.Feature.Player.PlayerInput;
using Scenes.Gameplay.Feature.Progress;
using Scenes.Gameplay.Feature.RageMode.Services;
using Scenes.Gameplay.Feature.Reset.Services;
using Scenes.Gameplay.StateMachine.States;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Bootstrap
{
	public class GameplayInstaller : MonoInstaller
	{
		[SerializeField] private Camera mainCamera;
		[SerializeField, SerializeReference] private IAnimation cameraAnimation;

		[Header("Progress")]
		[SerializeField] private int winProgress = 100;

		[Header("Health")]
		[SerializeField] private HealthConfig config;

		[Header("Autopilot")]
		[SerializeField] private AutopilotBonusStrategyDatabase strategyDatabase;

		public override void InstallBindings()
		{
			RouterInstaller.Install(Container);

			BindCameraAnimator();
			BindCamera();

			BindResetService();
			BindRageModeService();

			BindAutopilotService();
			BindAutopilotStrategyFactory();

			BindProgressController();
			BindHealthController();
			BindBoundaryValidator();

			BindTimeProvider();
			BindInput();
		}

		private void BindAutopilotService()
		{
			Container.BindInterfacesTo<AutopilotService>().AsSingle();
		}

		private void BindAutopilotStrategyFactory()
		{
			Container.Bind<IAutopilotStrategyFactory>()
				.To<AutopilotStrategyFactory>()
				.AsSingle()
				.WithArguments(strategyDatabase);
		}

		private void BindRageModeService()
		{
			Container.Bind<IRageModeService>().To<RageModeService>().AsSingle();
		}

		private void BindCamera()
		{
			Container.BindInstance(mainCamera).AsSingle();
		}

		private void BindCameraAnimator()
		{
			Container.Bind<IAnimation>().FromInstance(cameraAnimation).AsSingle().WhenInjectedInto<GameplayState>();
		}

		private void BindResetService()
		{
			Container.Bind<IResetService>().To<ResetService>().AsSingle();
		}

		private void BindBoundaryValidator()
		{
			Container.BindInterfacesAndSelfTo<BoundaryValidator>().AsSingle();
		}

		private void BindHealthController()
		{
			Container.BindInterfacesAndSelfTo<HealthController>()
				.AsSingle()
				.WithArguments(config);
		}

		private void BindProgressController()
		{
			Container.Bind<IProgressController>()
				.To<ProgressController>()
				.AsSingle()
				.WithArguments(winProgress);
		}

		private void BindInput()
		{
			Container.BindInterfacesTo<MouseInput>()
				.AsSingle()
				.WithArguments(mainCamera);
		}

		private void BindTimeProvider()
		{
			Container.Bind<ITimeProvider>()
				.To<GameplayTimeProvider>()
				.AsSingle();
		}
	}
}
