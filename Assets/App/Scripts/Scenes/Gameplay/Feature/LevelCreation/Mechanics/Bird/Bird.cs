using Cysharp.Threading.Tasks;
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
		[SerializeField] private BirdVisual visual;

		public event Action OnDeath;

		private int damage = DEFAULT_DAMAGE;
		private int health;

		public BirdVisual Visual { get => visual; }
		public int Health
		{
			get => health;
			set
			{
				health = value;
				if (health <= 0)
				{
					OnDeath?.Invoke();
				}
			}
		}

		private async void OnCollisionEnter2D(Collision2D collision)
		{
			await ApplyDamage(collision.gameObject);
		}

		private async void OnTriggerEnter2D(Collider2D collision)
		{
			await ApplyDamage(collision.gameObject);
		}

		public void Move(Vector2 newPosition)
		{
			transform.position = newPosition;
		}


		private async UniTask ApplyDamage(GameObject gameObject)
		{
			if (gameObject.TryGetComponent(out IDamager damager))
			{
				await ReduceHealth(damage);
			}
		}

		private async UniTask ReduceHealth(int value)
		{
			health -= value;
			if (health <= 0)
			{
				await visual.DestroyAnimation();
				OnDeath?.Invoke();
				visual.ResetVisual();
				return;
			}
			await visual.ApplyDamageAnimation();
		}

		public void ActivateRageMode()
		{
			birdCollider.isTrigger = true;
			damage = int.MaxValue;
		}

		public void DeactivateRageMode()
		{
			birdCollider.isTrigger = false;
			damage = DEFAULT_DAMAGE;
		}
	}
}

