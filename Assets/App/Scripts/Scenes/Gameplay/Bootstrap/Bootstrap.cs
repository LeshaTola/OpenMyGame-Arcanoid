using Features.Bootstrap;
using Module.TimeProvider;
using Scenes.Gameplay.Feature.PlayerInput;
using Scenes.Gameplay.Feature.ShipLogic;
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

		[SerializeField] List<SerializableInterface<IInitializable>> initializables;

		private void Awake()
		{
			IInput input = new MouseInput(mainCamera);
			ITimeProvider timeProvider = new GameplayTimeProvider();

			movement.Init(input, timeProvider);

			foreach (var initializable in initializables)
			{
				initializable.Value.Init();
			}
		}
	}
}