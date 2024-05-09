using System.Collections.Generic;
using App.Scripts.Features.Bootstrap;
using TNRD;
using UnityEngine;

namespace App.Scripts.Scenes.Main.Bootstrap
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