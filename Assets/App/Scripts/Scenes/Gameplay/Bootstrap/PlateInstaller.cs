using Scenes.Gameplay.Feature.Player;
using Scenes.Gameplay.Feature.Player.Configs;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Bootstrap
{
	public class PlateInstaller : MonoInstaller
	{
		[SerializeField] private MovementConfig config;
		[SerializeField] private Rigidbody2D rb;

		public override void InstallBindings()
		{
			BindMovement();
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
