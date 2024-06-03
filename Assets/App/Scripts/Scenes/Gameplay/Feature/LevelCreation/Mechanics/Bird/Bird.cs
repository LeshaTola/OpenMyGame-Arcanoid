using Scenes.Gameplay.Feature.Damage;
using System;
using UnityEngine;

namespace Scenes.Gameplay.Feature.LevelCreation.Mechanics.Bird
{
	public class Bird : MonoBehaviour, IDamageable
	{
		public event Action OnDeath;

		private int health;

		public void Init(int health)
		{
			this.health = health;
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			ApplyDamage(collision.gameObject);
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			ApplyDamage(collision.gameObject);
		}

		public void Move(Vector2 newPosition)
		{
			transform.position = newPosition;
		}

		private void ReduceHealth(int value)
		{
			health -= value;
			if (health <= 0)
			{
				OnDeath?.Invoke();
			}
		}

		private void ApplyDamage(GameObject gameObject)
		{
			if (gameObject.TryGetComponent(out IDamager damager))
			{
				ReduceHealth(1);
			}
		}
	}
}

