using Scenes.Gameplay.Feature.Health.UI.HealthIcon;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Health.UI
{
	public class HealthBarUI : MonoBehaviour, IHealthBarUI
	{
		[SerializeField] private HealthIconUI healthIconTemplate;
		[SerializeField] private Transform container;

		private List<HealthIconUI> healthIcons = new();

		public void DeactivateAmount(int currentHealth, int prevHealth)
		{
			currentHealth -= 1;
			prevHealth -= 1;
			currentHealth = Mathf.Clamp(currentHealth, 0, healthIcons.Count);
			prevHealth = Mathf.Clamp(prevHealth, 0, healthIcons.Count);

			for (int i = currentHealth; i < prevHealth; i++)
			{
				healthIcons[i].Hide();
			}
		}

		public void ActivateAmount(int currentHealth, int prevHealth)
		{
			currentHealth -= 1;
			prevHealth -= 1;
			currentHealth = Mathf.Clamp(currentHealth, 0, healthIcons.Count);
			prevHealth = Mathf.Clamp(prevHealth, 0, healthIcons.Count);

			for (int i = prevHealth; i < currentHealth; i++)
			{
				healthIcons[i].Show();
			}
		}

		public void CreateUI(int maxHealth)
		{
			healthIcons = new(maxHealth - 1);
			for (int i = 0; i < maxHealth - 1; i++)
			{
				var healthIcon = Instantiate(healthIconTemplate, container);
				healthIcons.Add(healthIcon);
			}

			DeactivateAmount(maxHealth, 0);

			healthIcons.Reverse();
		}

	}
}