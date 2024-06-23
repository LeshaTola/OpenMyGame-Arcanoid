using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Progress
{
	public class ProgressUI : MonoBehaviour, IProgressUI
	{
		[SerializeField] private TextMeshProUGUI progressText;
		[SerializeField] private float progressAnimation = 0.3f;

		private int prevProgress = 0;

		public void UpdateProgress(int progress)
		{
			if (progress < 0 || progress > 100)
			{
				return;
			}

			DOVirtual.Int(prevProgress, progress, progressAnimation, (value) =>
			{
				progressText.text = value.ToString() + "%";
			});

			prevProgress = progress;
		}
	}
}