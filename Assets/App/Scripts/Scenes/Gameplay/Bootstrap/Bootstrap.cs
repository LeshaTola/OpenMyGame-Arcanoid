using System.Collections.Generic;
using App.Scripts.Features.Bootstrap;
using App.Scripts.Module.TimeProvider;
using App.Scripts.Scenes.Gameplay.Feature.PlayerInput;
using App.Scripts.Scenes.Gameplay.Feature.ShipLogic;
using TNRD;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Bootstrap
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