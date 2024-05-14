using Module.ObjectPool;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Player.Ball
{
	public class Ball : MonoBehaviour, IPooledObject<Ball>
	{
		[SerializeField] private BallMovement movement;

		private IPool<Ball> pool;

		public BallMovement Movement => movement;

		private void OnCollisionEnter2D(Collision2D collision)
		{
			Vector2 newDirection = movement.Direction;
			if (!collision.gameObject.TryGetComponent(out Plate player))
			{
				newDirection = movement.GetValidDirection();
			}
			movement.Push(newDirection);
		}

		public void Release()
		{
			if (pool != null)
			{
				pool.Release(this);
				return;
			}
			Destroy(gameObject);
		}

		public void OnGet(IPool<Ball> pool)
		{
			this.pool = pool;
		}

		public void OnRelease()
		{
		}
	}
}
