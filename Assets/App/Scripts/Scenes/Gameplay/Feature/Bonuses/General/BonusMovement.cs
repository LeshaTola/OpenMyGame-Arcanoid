using Scenes.Gameplay.Feature.Bonuses.Configs;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Bonuses.General
{
	public class BonusMovement : MonoBehaviour
	{
		[SerializeField] private BonusMovementConfig config;

		public void Move(float deltaTime)
		{
			transform.position = Vector2.Lerp(transform.position, transform.position + Vector3.down * config.FallSpeed, deltaTime);
		}
	}
}
