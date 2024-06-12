using Module.ObjectPool.KeyPools;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Player.Ball.Providers.CollisionParticles
{
	[CreateAssetMenu(fileName = "CollisionToParticlesDatabase", menuName = "Dictionaries/CollisionToParticles")]
	public class CollisionToParticlesDatabase : SerializedScriptableObject
	{
		[SerializeField] private ParticlesDatabase particlesDatabase;

		[ShowIf("@particlesDatabase!=null")]
		[FoldoutGroup("Key\\Value")]
		[SerializeField] private MonoBehaviour key;

		[ShowIf("@particlesDatabase!=null")]
		[FoldoutGroup("Key\\Value")]
		[ValueDropdown("@particlesDatabase.GetKeys()")]
		[SerializeField] private string value;

		[ReadOnly]
		[SerializeField] private Dictionary<MonoBehaviour, string> objectToParticle;

		public Dictionary<MonoBehaviour, string> ObjectToParticle { get => objectToParticle; }

		[Button]
		[ShowIf("@particlesDatabase!=null")]
		public void AddValue()
		{
			if (objectToParticle == null)
			{
				objectToParticle = new();
			}
			if (objectToParticle.ContainsKey(key))
			{
				Debug.LogWarning("value with such key is already exists");
				return;
			}

			objectToParticle.Add(key, value);
		}

		[Button]
		[ShowIf("@particlesDatabase!=null")]
		public void RemoveValue()
		{
			if (!objectToParticle.ContainsKey(key))
			{
				Debug.LogWarning("value with such key is not exists");
				return;
			}

			objectToParticle.Remove(key);
		}

	}
}
