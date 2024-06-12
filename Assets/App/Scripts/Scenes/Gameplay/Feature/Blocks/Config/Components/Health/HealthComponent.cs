using Scenes.Gameplay.Feature.Blocks.Config.Components.General;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Scenes.Gameplay.Feature.Blocks.Config.Components.Health
{
	public class HealthComponent : General.Component
	{
		public event Action<Block> OnDeath;

		[SerializeField] private int maxHealth;
		[SerializeField] private CracksDictionary cracksDictionary;
		[SerializeField] List<IComponent> deathComponents;
		[SerializeField] List<IComponent> damageComponents;

		private int health;

		public List<IComponent> DeathComponents { get => deathComponents; }
		public int Health { get => health; }

		public override void Init(Block block)
		{
			base.Init(block);
			health = maxHealth;
			foreach (var component in deathComponents)
			{
				component.Init(block);
			}
			foreach (var component in damageComponents)
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

		public void SetHealth(int health)
		{
			this.health = health;
			if (health <= 0)
			{
				Kill();
				return;
			}

			SetCrack();
		}

		public void ReduceHealth(int damage)
		{
			health -= damage;
			if (health <= 0)
			{
				Kill();
				return;
			}

			foreach (IComponent component in damageComponents)
			{
				component.Execute();
			}


			SetCrack();
		}

		private void SetCrack()
		{
			if (health >= maxHealth)
			{
				return;
			}

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
