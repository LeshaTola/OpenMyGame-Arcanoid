using UnityEngine;

namespace Scenes.Gameplay.Feature.Bonuses.Configs
{
	[CreateAssetMenu(fileName = "BonusMovementConfig", menuName = "Configs/Bonus/Movement")]

	public class BonusMovementConfig : ScriptableObject
	{
		[SerializeField] private float fallSpeed;

		public float FallSpeed { get => fallSpeed; }
	}
}
