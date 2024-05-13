using Features.UI.Animations.SwapColor;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Health.UI.HealthIcon
{
	public class HealthIconUI : MonoBehaviour, IHealthIconUI
	{
		[SerializeField] private StandardSwapColorAnimation iconAnimation;

		public void Show()
		{
			iconAnimation.Show();
		}

		public void Hide()
		{
			iconAnimation.Hide();
		}
	}


}