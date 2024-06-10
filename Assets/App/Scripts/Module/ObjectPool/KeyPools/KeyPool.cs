using Module.ObjectPool;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.App.Scripts.Module.ObjectPool.KeyPools
{
	public class KeyPool<T> where T : class
	{
		public Dictionary<string, IPool<T>> poolsDictionary;

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
