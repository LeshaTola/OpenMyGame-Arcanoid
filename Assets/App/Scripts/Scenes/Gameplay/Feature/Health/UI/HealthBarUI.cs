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
			for (int i = currentHealth; i < prevHealth; i++)
			{
				healthIcons[i].Hide();
			}
		}

		public void ActivateAmount(int currentHealth, int prevHealth)
		{
			for (int i = prevHealth; i < currentHealth; i++)
			{
				healthIcons[i].Show();
			}
		}

		public void CreateUI(int maxHealth)
		{
			healthIcons = new(maxHealth);
			for (int i = 0; i < maxHealth; i++)
			{
				var healthIcon = Instantiate(healthIconTemplate, container);
				healthIcons.Add(healthIcon);
			}

			DeactivateAmount(0, maxHealth);

			healthIcons.Reverse();
		}

	}
}