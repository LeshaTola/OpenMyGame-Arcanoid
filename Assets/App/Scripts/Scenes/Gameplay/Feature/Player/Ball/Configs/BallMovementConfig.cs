using UnityEngine;

namespace Scenes.Gameplay.Feature.Player.Configs
{
	[CreateAssetMenu(fileName = "BallMovementConfig", menuName = "Configs/Ball/Movement")]
	public class BallMovementConfig : ScriptableObject
	{
		[SerializeField] private float speed;
		[SerializeField, Min(1f)] private float speedMultiplier = 1f;
		[SerializeField] private float minAngle;

		public float Speed { get => speed; }
		public float SpeedMultiplier { get => speedMultiplier; }
		public float MinAngle { get => minAngle; }
	}
}
