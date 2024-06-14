using Scenes.Gameplay.Feature.Player.Configs;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Player.Ball
{
	public class BallMovement : MonoBehaviour
	{
		[SerializeField] private BallMovementConfig config;
		[SerializeField] private Rigidbody2D rb;

		public Vector2 Direction { get => rb.velocity.normalized; }
		public Rigidbody2D Rb { get => rb; }
		public BallMovementConfig Config { get => config; }

		public void Push(Vector2 direction)
		{
			Push(direction, 0);
		}

		public void Push(Vector2 direction, float factor, float multiplier = 1)
		{
			float additionalSpeed = Config.Speed * (Config.SpeedMultiplier - 1) * factor;
			rb.velocity = direction.normalized * (Config.Speed + additionalSpeed) * multiplier;
		}
	}
}