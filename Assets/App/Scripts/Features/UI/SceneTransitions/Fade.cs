using DG.Tweening;
using System;
using UnityEngine;

namespace Features.UI.SceneTransitions
{
	public class Fade : MonoBehaviour, ISceneTransition
	{
		[SerializeField] private float fadeTime;

		private Tween tween;

		public void PlayOn(Action action = null)
		{
			CleanUp();

			gameObject.SetActive(true);
			transform.localScale = Vector3.zero;

			tween = transform.DOScale(1f, fadeTime).SetUpdate(true);
			tween.onComplete += () => action?.Invoke();
		}

		public void PlayOff(Action action = null)
		{
			CleanUp();

			gameObject.SetActive(true);
			transform.localScale = Vector3.one;

			tween = transform.DOScale(0f, fadeTime).SetUpdate(true);
			tween.onComplete += () =>
			{
				action?.Invoke();
				gameObject.SetActive(false);
			};
		}

		private void CleanUp()
		{
			tween.Kill();
		}

		private void OnDestroy()
		{
			CleanUp();
		}
	}
}