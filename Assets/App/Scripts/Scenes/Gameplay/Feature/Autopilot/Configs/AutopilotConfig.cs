using UnityEngine;

namespace Scenes.Gameplay.Feature.Autopilot.Configs
{
	[CreateAssetMenu(fileName = "AutopilotConfig", menuName = "Configs/Autopilot")]
	public class AutopilotConfig : ScriptableObject
	{
		[SerializeField] private float maneuverDistance = 0.2f;
		[SerializeField] private float autopilotTargetDistance = 4;
		[SerializeField] private float deadZone = 0.3f;
		[SerializeField] private AutopilotPriorityConfig autopilotPriorityConfig;

		public float ManeuverDistance { get => maneuverDistance; }
		public float AutopilotTargetDistance { get => autopilotTargetDistance; }
		public AutopilotPriorityConfig AutopilotPriorityConfig { get => autopilotPriorityConfig; }
		public float DeadZone { get => deadZone; }
	}
}
