using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Feature.Player.Ball
{
	public class BallMovement : MonoBehaviour
	{
		[SerializeField] private float speed;
		[SerializeField] private float minAngle;
		[SerializeField] private Rigidbody2D rb;

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
			//rb.velocity = Vector2.zero;
			//rb.AddForce(direction.normalized * speed, ForceMode2D.Impulse);
		}

		public void ValidateDirection()
		{
			rb.velocity = ValidateDirection(rb.velocity);
		}

		public Vector2 ValidateDirection(Vector2 direction)
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