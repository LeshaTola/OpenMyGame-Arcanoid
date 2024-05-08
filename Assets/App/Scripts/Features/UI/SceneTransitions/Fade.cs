using DG.Tweening;
using System;
using UnityEngine;

namespace Features.UI.SceneTransitions
{
	public class Fade : MonoBehaviour, ISceneTransition
	{
		[SerializeField] private float fadeTime;

		public void PlayOn(Action action = null)
		{
			gameObject.SetActive(true);
			transform.localScale = Vector3.zero;
			transform.DOScale(1f, fadeTime).SetUpdate(true).onComplete += () => action?.Invoke();
		}

		public void PlayOff(Action action = null)
		{
			gameObject.SetActive(true);
			transform.localScale = Vector3.one;
			transform.DOScale(0f, fadeTime).SetUpdate(true).onComplete += () =>
			{
				action?.Invoke();
				gameObject.SetActive(false);
			};
		}

		private void OnDestroy()
		{
			transform.DOKill();
		}
	}
}