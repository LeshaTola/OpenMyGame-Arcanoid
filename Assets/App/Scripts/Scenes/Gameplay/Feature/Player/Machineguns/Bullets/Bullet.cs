using Module.ObjectPool;
using Scenes.Gameplay.Feature.Blocks;
using Scenes.Gameplay.Feature.Blocks.Config.Components;
using Scenes.Gameplay.Feature.Blocks.Config.Components.Health;
using Scenes.Gameplay.Feature.Field;
using System;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Player.Machineguns.Bullets
{
	public class Bullet : MonoBehaviour, IPooledObject<Bullet>
	{
		[SerializeField] private Rigidbody2D rb;

		private BulletParams bulletParams;
		private IPool<Bullet> pool;

		public void Setup(BulletParams bulletParams)
		{
			this.bulletParams = bulletParams;
		}

		public void Shoot(Vector2 direction, float multiplier = 1)
		{
			rb.velocity = direction.normalized * bulletParams.Speed * multiplier;
		}

		public void OnGet(IPool<Bullet> pool)
		{
			this.pool = pool;
		}

		public void OnRelease()
		{
		}

		public void Release()
		{
			pool.Release(this);
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			Block block = collision.gameObject.GetComponent<Block>();
			if (block != null || collision.TryGetComponent(out Wall wall))
			{
				Release();
			}

			if (block == null || !block.Config.TryGetComponent(out HealthComponent healthComponent))
			{
				return;
			}

			if (CheckArmorPiercing(block))
			{
				healthComponent.ReduceHealth(bulletParams.Damage);
			}
		}

		private bool CheckArmorPiercing(Block block)
		{
			if (!bulletParams.IsArmorPiercing)
			{
				if (!block.Config.TryGetComponent(out CollisionComponent collisionComponent)
					|| !block.Config.TryGetComponent(out ReduceHealthComponent reduceHealthComponent,
									 collisionComponent.CollisionComponents))
				{
					return false;
				}
			}
			return true;
		}
	}

	[Serializable]
	public struct BulletParams
	{
		public int Damage;
		public float Speed;
		public bool IsArmorPiercing;
	}
}
