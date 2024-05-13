using System.Collections.Generic;
using UnityEngine;

namespace Module.ObjectPool
{
	public class MonoBehObjectPool<T> : IPool<T> where T : MonoBehaviour
	{
		private ObjectPool<T> core;

		public IReadOnlyCollection<T> Active { get => core.Active; }

		public MonoBehObjectPool(T objectTemplate, int startCount, Transform parent = null)
		{
			core = new(
				() =>
				{
					T pooledObject = GameObject.Instantiate(objectTemplate, parent);
					pooledObject.gameObject.SetActive(false);
					return pooledObject;
				},
				null,
				null,
				startCount
				);
		}

		public T Get()
		{
			T pooledObject = core.Get();
			pooledObject.gameObject.SetActive(true);
			return pooledObject;
		}

		public void Release(T pooledObject)
		{
			pooledObject.gameObject.SetActive(false);
			core.Release(pooledObject);
		}
	}
}
