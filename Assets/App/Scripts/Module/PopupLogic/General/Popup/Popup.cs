using Module.PopupLogic.General.Controller;
using TNRD;
using UnityEngine;
using UnityEngine.UI;

namespace Module.PopupLogic.General.Popups
{
	[RequireComponent(typeof(Canvas), typeof(GraphicRaycaster))]
	public abstract class Popup : MonoBehaviour, IPopup
	{
		[SerializeField] protected SerializableInterface<IPopupAnimation> popupAnimation;
		[SerializeField] protected GraphicRaycaster raycaster;
		[SerializeField] protected Canvas canvas;

		public IPopupController Controller { get; private set; }
		public Canvas Canvas { get => canvas; }

		public void Init(IPopupController controller)
		{
			Controller = controller;
		}

		public virtual void Hide()
		{
			Deactivate();
			popupAnimation.Value.Hide(() =>
			{
				Controller.RemoveActivePopup(this);
				gameObject.SetActive(false);
			});
		}

		public virtual void Show()
		{
			gameObject.SetActive(true);
			Controller.AddActivePopup(this);
			popupAnimation.Value.Show(() =>
			{
				Activate();
			});
		}

		public void Activate()
		{
			raycaster.enabled = true;
		}

		public void Deactivate()
		{
			raycaster.enabled = false;
		}
	}
}

