using App.Scripts.Scenes.Gameplay.Feature.Blocks.Config.Components.General;
using System;
using System.Collections.Generic;


namespace App.Scripts.Scenes.Gameplay.Feature.Blocks.Config.Components.Health
{
	public class HealthComponent : Component
	{
		public event Action<Block> OnDeath;

		[UnityEngine.SerializeField] private int health;
		[UnityEngine.SerializeField] private CracksDictionary cracksDictionary;
		[UnityEngine.SerializeField] List<IComponent> deathComponents;

		public override void Init(Block block)
		{
			base.Init(block);
			foreach (var component in deathComponents)
			{
				component.Init(block);
			}
		}

		public void Kill()
		{
			health = 0;
			OnDeath?.Invoke(Block);

			foreach (IComponent component in deathComponents)
			{
				component.Execute();
			}
		}

		public void ReduceHealth(int damage)
		{
			health -= damage;

			if (health <= 0)
			{
				Kill();
				return;
			}

			SetCrack();
		}

		private void SetCrack()
		{
			foreach (var key in cracksDictionary.Cracks.Keys)
			{
				if (key.IsValid(health))
				{
					Block.Visual.SetCrack(cracksDictionary.Cracks[key]);
					break;
				}
			}
		}
	}
}
