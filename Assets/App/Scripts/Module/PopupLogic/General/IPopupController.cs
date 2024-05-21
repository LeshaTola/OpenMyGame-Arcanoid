using UnityEngine;

namespace Module.PopupLogic.General
{
	public interface IPopupController
	{
		void HidePopup();
		T ShowPopup<T>() where T : MonoBehaviour, IPopup;
	}
}