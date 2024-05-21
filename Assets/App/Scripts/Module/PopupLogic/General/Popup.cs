using TNRD;
using UnityEngine;

namespace Module.PopupLogic.General
{
	[RequireComponent(typeof(CanvasGroup))]
	public abstract class Popup : MonoBehaviour, IPopup
	{
		[SerializeField] private SerializableInterface<IPopupAnimation> popupAnimation;
		[SerializeField] private CanvasGroup canvasGroup;

		public void Activate()
		{
			canvasGroup.blocksRaycasts = true;
		}

		public void Deactivate()
		{
			canvasGroup.blocksRaycasts = false;
		}

		public virtual void Hide()
		{
			Deactivate();
			popupAnimation.Value.Hide(() =>
			{
				gameObject.SetActive(false);
			});
		}

		public virtual void Show()
		{
			gameObject.SetActive(true);
			popupAnimation.Value.Show(() =>
			{
				Activate();
			});
		}
	}
}

