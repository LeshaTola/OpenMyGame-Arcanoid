using TMPro;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Feature.Progress
{
	public class ProgressUI : MonoBehaviour, IProgressUI
	{
		[SerializeField] private TextMeshProUGUI progressText;

		public void UpdateProgress(int progress)
		{
			if (progress < 0 || progress > 100)
			{
				return;
			}
			progressText.text = progress.ToString() + "%";
		}
	}
}