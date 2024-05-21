using Module.PopupLogic.General.Popup;
using UnityEngine;

namespace Module.PopupLogic.General.Factory
{
	public interface IPopupFactory
	{
		T GetPopup<T>() where T : MonoBehaviour, IPopup;
	}
}

