using Cysharp.Threading.Tasks;
using Module.PopupLogic.General.Controller;
using Sirenix.OdinInspector;
using TNRD;
using UnityEngine;
using UnityEngine.UI;

namespace Module.PopupLogic.General.Popups
{
	[RequireComponent(typeof(Canvas), typeof(GraphicRaycaster))]
	public abstract class Popup : MonoBehaviour, IPopup
	{
		[FoldoutGroup("General")]
		[SerializeField] protected SerializableInterface<IPopupAnimation> popupAnimation;
		[FoldoutGroup("General")]
		[SerializeField] protected GraphicRaycaster raycaster;
		[FoldoutGroup("General")]
		[SerializeField] protected Canvas canvas;

		public IPopupController Controller { get; private set; }
		public Canvas Canvas { get => canvas; }

		public void Init(IPopupController controller)
		{
			Controller = controller;
		}

		public async virtual UniTask Hide()
		{
			Deactivate();

			await popupAnimation.Value.Hide();
			Controller.RemoveActivePopup(this);
			gameObject.SetActive(false);
		}

		public async virtual UniTask Show()
		{
			gameObject.SetActive(true);
			Controller.AddActivePopup(this);

			await popupAnimation.Value.Show();
			Activate();
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

