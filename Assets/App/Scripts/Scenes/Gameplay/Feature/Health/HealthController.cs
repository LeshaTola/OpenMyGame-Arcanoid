using Features.Bootstrap;
using Scenes.Gameplay.Feature.Health.Configs;
using Scenes.Gameplay.Feature.Health.UI;
using System;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Health
{
	public class HealthController : MonoBehaviour, IInitializable, IHealthController
	{
		public event Action OnDeath;

		[SerializeField] private HealthConfig config;
		[SerializeField] private HealthBarUI healthBarUI;

		private int currentHealth;

		public int MaxHealth { get => config.MaxHealth; }
		public int CurrentHealth { get => currentHealth; }

		public void Init()
		{
			healthBarUI.CreateUI(config.MaxHealth);
		}

		public void AddHealth(int health)
		{
			if (health <= 0)
			{
				return;
			}
			int prevHealth = currentHealth;
			currentHealth += health;

			if (currentHealth > config.MaxHealth)
			{
				currentHealth = config.MaxHealth;
			}

			healthBarUI.ActivateAmount(currentHealth, prevHealth);
		}

		public void ReduceHealth(int health)
		{
			if (health <= 0)
			{
				return;
			}
			int prevHealth = currentHealth;
			currentHealth -= health;

			if (currentHealth <= 0)
			{
				currentHealth = 0;
				OnDeath?.Invoke();
			}

			healthBarUI.DeactivateAmount(currentHealth, prevHealth);
		}

		public void ResetHealth()
		{
			AddHealth(config.Health);
		}
	}
}