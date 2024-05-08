using System.Collections.Generic;
using UnityEngine;

namespace Module.ObjectPool
{
	public class MonoBehObjectPool<T> where T : MonoBehaviour
	{
		private ObjectPool<T> core;

		public IReadOnlyCollection<T> Active { get => core.Active; }

		public MonoBehObjectPool(T objectTemplate, int startCount)
		{
			core = new(
				() => GameObject.Instantiate(objectTemplate),
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
