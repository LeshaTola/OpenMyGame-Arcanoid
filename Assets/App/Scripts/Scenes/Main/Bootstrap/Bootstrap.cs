using Features.Bootstrap;
using System.Collections.Generic;
using TNRD;
using UnityEngine;

namespace Scenes.Main.Bootstrap
{
	[AddComponentMenu("Scenes/Gameplay")]
	public class Bootstrap : MonoBehaviour
	{
		[SerializeField] List<SerializableInterface<IInitializable>> initializables;

		private void Awake()
		{
			foreach (var initializable in initializables)
			{
				initializable.Value.Init();
			}
		}
	}
}