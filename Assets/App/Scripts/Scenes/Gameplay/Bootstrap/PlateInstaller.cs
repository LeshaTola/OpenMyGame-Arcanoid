using Features.StateMachine;
using Scenes.Gameplay.Feature.Player;
using Scenes.Gameplay.Feature.Player.Configs;
using Scenes.Gameplay.Feature.Player.Machineguns;
using Scenes.Gameplay.Feature.Player.Providers;
using Scenes.Gameplay.Feature.Reset;
using Scenes.Gameplay.Feature.Reset.Services;
using Scenes.Gameplay.StateMachine.States;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Bootstrap
{
	public class PlateInstaller : MonoInstaller
	{
		[SerializeField] private MovementConfig config;
		[SerializeField] private Plate plate;
		[SerializeField] private Machinegun machinegun;

		public override void InstallBindings()
		{
			BindMachinegun();
			BindMovement();
			BindPlate();
			BindPlateSizeProvider();
		}

		private void BindPlateSizeProvider()
		{
			Container.Bind<IPlateSizeProvider>().To<PlateSizeProvider>().AsSingle();
		}

		private void BindMachinegun()
		{
			Container.BindInstance(machinegun).AsSingle();
			Container.Bind<IUpdatable>().FromInstance(machinegun).WhenInjectedInto<GameplayState>();
		}

		private void BindPlate()
		{
			Container.BindInstance(plate).AsSingle();
			Container.Bind<IUpdatable>().FromInstance(plate).WhenInjectedInto<GameplayState>();
			Container.Bind<IResetable>().FromInstance(plate).WhenInjectedInto<IResetService>();
		}

		private void BindMovement()
		{
			Container.Bind<IMovement>()
				.To<Movement>()
				.AsSingle()
				.WithArguments(config, plate.transform);
		}
	}
}
