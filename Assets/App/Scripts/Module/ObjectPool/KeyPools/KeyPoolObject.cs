using System;
using UnityEngine;

namespace Module.ObjectPool.KeyPools
{
	[Serializable]
	public struct KeyPoolObject<T> where T : MonoBehaviour
	{
		public string Key;
		public int PreloadCount;
		[SerializeField] public T Template;
		public Transform Container;
	}
}
