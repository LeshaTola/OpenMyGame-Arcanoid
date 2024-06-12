using UnityEngine;

namespace Scenes.Gameplay.Feature.Player.Configs
{
	[CreateAssetMenu(fileName = "MovementConfig", menuName = "Configs/Ship/Movement")]
	public class MovementConfig : ScriptableObject
	{
		[SerializeField, Min(0)] private float speed;
		[SerializeField, Min(0)] private float drag;

		public float Speed { get => speed; }
		public float Drag { get => drag; }

	}
}
