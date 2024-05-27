using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Reset.Services
{
	public class ResetService : SerializedMonoBehaviour, IResetService
	{
		[SerializeField] List<IResetable> resetables;
		public void Reset()
		{
			foreach (var resetable in resetables)
			{
				resetable.Reset();
			}
		}
	}
}
