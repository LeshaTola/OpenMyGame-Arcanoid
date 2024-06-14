﻿using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Module.ObjectPool.KeyPools
{
	[CreateAssetMenu(fileName = "ParticlesDatabase", menuName = "Dictionaries/Particles")]
	public class ParticlesDatabase : SerializedScriptableObject
	{
		[SerializeField] Dictionary<string, PoolObject<PooledParticle>> particles;

		public Dictionary<string, PoolObject<PooledParticle>> Particles { get => particles; }

		public IEnumerable<string> GetKeys()
		{
			if (particles == null)
			{
				return null;
			}
			return new List<string>(particles.Keys);
		}
	}

}
