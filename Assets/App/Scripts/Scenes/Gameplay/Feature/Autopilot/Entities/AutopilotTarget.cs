using UnityEngine;

namespace Scenes.Gameplay.Feature.Autopilot.Services.Entities
{
	public class AutopilotTarget : MonoBehaviour
	{
		[SerializeField, Min(0)] private int priority;

		public int Priority { get => priority; }
	}
}