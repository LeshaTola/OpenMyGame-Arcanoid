using Module.ObjectPool;
using Module.ObjectPool.KeyPools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Blocks.Config.Components.Partcles
{
	public class SpawnParticlesComponent : General.Component
	{
		[SerializeField] private bool changeColor = false;
		[ShowIf("@changeColor == true")]
		[SerializeField] private Color particleColor;

		[SerializeField] private ParticlesDatabase particlesDatabase;
		[ShowIf("@particlesDatabase!=null")]
		[ValueDropdown("@particlesDatabase.GetKeys()")]
		[SerializeField] private string particleKey;

		public override void Execute()
		{
			base.Execute();
		}

		public void SpawnParticle(Vector3 position, Vector2 direction, float sizeMultiplier = 1)
		{
			PooledParticle particle = Block.KeyPool.Get(particleKey);
			if (changeColor)
			{
				particle.ChangeColor(particleColor);
			}

			particle.Resize(sizeMultiplier);
			particle.transform.position = position;
			particle.transform.up = direction;
			particle.Particle.Play();
		}
	}
}
