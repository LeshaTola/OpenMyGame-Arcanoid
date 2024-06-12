using System.Collections.Generic;
using UnityEngine;

namespace Module.ObjectPool.KeyPools
{
	public class KeyPool<T> where T : MonoBehaviour
	{
		private Dictionary<string, IPool<T>> poolsDictionary = new();

		public KeyPool(List<KeyPoolObject<T>> pooledObjects)
		{
			foreach (var pooledObject in pooledObjects)
			{
				var pool = new MonoBehObjectPool<T>(pooledObject.Template,
										pooledObject.PreloadCount,
										pooledObject.Container);
				poolsDictionary.Add(pooledObject.Key, pool);
			}
		}

		public void AddPool(string key, IPool<T> pool)
		{
			if (poolsDictionary.ContainsKey(key))
			{
				Debug.LogError("Pool with such key is already exist");
				return;
			}
			poolsDictionary.Add(key, pool);
		}

		public T Get(string key)
		{
			if (poolsDictionary.TryGetValue(key, out IPool<T> correctPool))
			{
				return correctPool.Get();
			}

			Debug.LogError($"There is no such pool key: {key}");
			return null;
		}

		public void Release(string key, T pooledObject)
		{
			if (poolsDictionary.TryGetValue(key, out IPool<T> correctPool))
			{
				correctPool.Release(pooledObject);
			}
			Debug.LogError($"There is no such pool key: {key}");
		}
	}
}
