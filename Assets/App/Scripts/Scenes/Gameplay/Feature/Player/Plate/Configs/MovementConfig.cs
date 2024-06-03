using UnityEngine;

namespace Scenes.Gameplay.Feature.Player.Configs
{
	[CreateAssetMenu(fileName = "MovementConfig", menuName = "Configs/Ship/Movement")]
	public class MovementConfig : ScriptableObject
	{
		[SerializeField, Min(0)] private float speed;
		[SerializeField, Min(0)] private float drag;
		[SerializeField, Min(0)] private float deceleration;
		[SerializeField, Min(0)] private float acceleration;
		[SerializeField, Range(0, 0.5f)] private float deadZone;

		public float Speed { get => speed; }
		public float Drag { get => drag; }
		public float DeadZone { get => deadZone; }
		public float Deceleration { get => deceleration; }
		public float Acceleration { get => acceleration; }
	}
}
