using System.Collections.Generic;
using App.Scripts.Features.Bootstrap;
using App.Scripts.Module.TimeProvider;
using App.Scripts.Scenes.Gameplay.Feature.Player;
using App.Scripts.Scenes.Gameplay.Feature.Player.PlayerInput;
using TNRD;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Bootstrap
{
	[AddComponentMenu("Scenes/Gameplay")]
	public class Bootstrap : MonoBehaviour
	{
		[SerializeField] private Camera mainCamera;

		[SerializeField] private Movement movement;
		[SerializeField] private Player player;

		[SerializeField] List<SerializableInterface<IInitializable>> initializables;

		private void Awake()
		{
			IInput input = new MouseInput(mainCamera);
			ITimeProvider timeProvider = new GameplayTimeProvider();

			player.Init(input);
			movement.Init(timeProvider);

			foreach (var initializable in initializables)
			{
				initializable.Value.Init();
			}
		}
	}
}