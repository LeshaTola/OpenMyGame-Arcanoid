using UnityEngine;

namespace Module.PopupLogic.General
{
	public interface IPopupController
	{
		void HidePopup();
		void ShowPopup<T>() where T : MonoBehaviour, IPopup;
	}
}