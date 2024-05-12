using App.Scripts.Module.ObjectPool;
using App.Scripts.Scenes.Gameplay.Feature.Field;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Feature.Player.Ball
{
	public class Ball : MonoBehaviour, IPooledObject
	{
		[SerializeField] private FieldController fieldController;
		[SerializeField] private BallMovement movement;

		private IPool<IPooledObject> pool;

		public BallMovement Movement => movement;


		private void Awake()
		{
			movement.Push(Vector2.up);
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			movement.ValidateDirection();
		}

		public void Release()
		{
			pool.Release(this);
		}

		public void OnGet(IPool<IPooledObject> pool)
		{
			this.pool = pool;
		}

		public void OnRelease()
		{
		}

	}
}
