using Features.Bootstrap;
using Module.TimeProvider;
using Scenes.Gameplay.Feature.Player;
using Scenes.Gameplay.Feature.Player.PlayerInput;
using System.Collections.Generic;
using TNRD;
using UnityEngine;

namespace Scenes.Gameplay.Bootstrap
{
	[AddComponentMenu("Scenes/Gameplay")]
	public class Bootstrap : MonoBehaviour
	{
		[SerializeField] private Camera mainCamera;

		[SerializeField] private Movement movement;
		[SerializeField] private InputController inputController;

		[SerializeField] List<SerializableInterface<IInitializable>> initializables;

		private void Awake()
		{
			IInput input = new MouseInput(mainCamera);
			ITimeProvider timeProvider = new GameplayTimeProvider();

			inputController.Init(input);
			movement.Init(timeProvider);

			foreach (var initializable in initializables)
			{
				initializable.Value.Init();
			}
		}
	}
}