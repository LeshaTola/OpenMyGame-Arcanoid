using UnityEngine;

namespace Module.ObjectPool
{
	public class PooledParticle : MonoBehaviour, IPooledObject<PooledParticle>
	{
		[SerializeField] private ParticleSystem particle;

		private IPool<PooledParticle> pool;

		public ParticleSystem Particle { get => particle; }

		private void OnParticleSystemStopped()
		{
			Release();
		}

		public void OnGet(IPool<PooledParticle> pool)
		{
			this.pool = pool;
		}

		public void OnRelease()
		{
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
	}
}
