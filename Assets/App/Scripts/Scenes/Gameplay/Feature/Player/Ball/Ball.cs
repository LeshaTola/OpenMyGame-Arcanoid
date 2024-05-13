using Module.ObjectPool;
using Scenes.Gameplay.Feature.Field;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Player.Ball
{
	public class Ball : MonoBehaviour, IPooledObject<Ball>
	{
		[SerializeField] private FieldController fieldController;
		[SerializeField] private BallMovement movement;

		private IPool<Ball> pool;

		public BallMovement Movement => movement;

		private void Awake()
		{
			movement.Push(Vector2.up);
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			Vector2 newDirection = movement.Direction;
			if (!collision.gameObject.TryGetComponent(out Player player))
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
