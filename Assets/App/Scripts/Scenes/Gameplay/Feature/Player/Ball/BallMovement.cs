using UnityEngine;

namespace Scenes.Gameplay.Feature.Player.Ball
{
	public class BallMovement : MonoBehaviour
	{
		[SerializeField] private float speed;
		[SerializeField] private float minAngle;
		[SerializeField] private Rigidbody2D rb;

		public Vector2 Direction { get => rb.velocity.normalized; }

		private Vector2[] axes =
		{
			Vector2.up,
			Vector2.down,
			Vector2.left,
			Vector2.right
		};

		public void Push(Vector2 direction)
		{
			rb.velocity = direction.normalized * speed;
		}

		public Vector2 GetValidDirection()
		{
			return GetValidDirection(rb.velocity);
		}

		public Vector2 GetValidDirection(Vector2 direction)
		{
			foreach (Vector2 axis in axes)
			{
				float angle = Vector2.Angle(direction, axis);
				if (angle < minAngle)
				{
					float rotateAngle = minAngle - angle;
					direction = Quaternion.Euler(0, 0, rotateAngle) * direction;
				}
			}
			return direction;
		}
	}
}