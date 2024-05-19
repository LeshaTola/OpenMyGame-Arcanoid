using System;
using UnityEngine;

namespace Module.PopupLogic.General
{
	public interface IPopupController
	{
		public event Action OnFirstPopupActivates;
		public event Action OnLastPopupDeactivates;

		void HidePopup();
		void ShowPopup<T>() where T : MonoBehaviour, IPopup;
	}
}