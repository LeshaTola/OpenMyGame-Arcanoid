using Features.StateMachine;
using Scenes.Gameplay.Feature.Player;
using Scenes.Gameplay.Feature.Player.Configs;
using Scenes.Gameplay.Feature.Player.Machineguns;
using Scenes.Gameplay.StateMachine.States;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Bootstrap
{
	public class PlateInstaller : MonoInstaller
	{
		[SerializeField] private MovementConfig config;
		[SerializeField] private Rigidbody2D rb;
		[SerializeField] private Plate plate;
		[SerializeField] private Machinegun machinegun;

		public override void InstallBindings()
		{
			BindMachinegun();
			BindMovement();
			BindPlate();
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
		}

		private void BindMovement()
		{
			Container.Bind<IMovement>()
				.To<Movement>()
				.AsSingle()
				.WithArguments(config, rb);
		}
	}
}
