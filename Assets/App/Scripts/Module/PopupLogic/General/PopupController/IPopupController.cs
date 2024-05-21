using Module.PopupLogic.General.Popup;
using UnityEngine;

namespace Module.PopupLogic.General.Controller
{
	public interface IPopupController
	{
		void HidePopup();
		T ShowPopup<T>() where T : MonoBehaviour, IPopup;
	}
}