using Scenes.Gameplay.Feature.Damage;
using Scenes.Gameplay.Feature.RageMode.Entities;
using System;
using UnityEngine;

namespace Scenes.Gameplay.Feature.LevelCreation.Mechanics.Bird
{
	public class Bird : MonoBehaviour, IDamageable, IEnraged
	{
		private const int DEFAULT_DAMAGE = 1;

		[SerializeField] private Collider2D birdCollider;

		public event Action OnDeath;

		private int damage = DEFAULT_DAMAGE;
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


		private void ApplyDamage(GameObject gameObject)
		{
			if (gameObject.TryGetComponent(out IDamager damager))
			{
				ReduceHealth(damage);
			}
		}

		private void ReduceHealth(int value)
		{
			health -= value;
			if (health <= 0)
			{
				OnDeath?.Invoke();
			}
		}

		public void ActivateRageMode()
		{
			birdCollider.isTrigger = true;
			damage = 999999;
		}

		public void DeactivateRageMode()
		{
			birdCollider.isTrigger = false;
			damage = DEFAULT_DAMAGE;
		}
	}
}

