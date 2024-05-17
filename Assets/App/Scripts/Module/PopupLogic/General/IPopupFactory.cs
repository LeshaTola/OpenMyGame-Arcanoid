using UnityEngine;

namespace Module.PopupLogic.General
{
	public interface IPopupFactory
	{
		T GetPopup<T>() where T : MonoBehaviour, IPopup;
	}
}

