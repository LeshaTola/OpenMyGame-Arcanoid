using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Module.PopupLogic.General
{
	public class PopupController : IPopupController
	{
		private IPopupFactory popupFactory;

		private Stack<IPopup> currentPopups;
		private List<IPopup> popups;

		public PopupController(IPopupFactory popupFactory)
		{
			this.popupFactory = popupFactory;
			currentPopups = new();
			popups = new();
		}

		public void ShowPopup<T>() where T : MonoBehaviour, IPopup
		{
			IPopup popup = GetPopup<T>();
			DeactivatePrevPopup();

			popup.Activate();
			popup.Show();
		}

		public void HidePopup()
		{
			if (currentPopups.Count <= 0)
			{
				return;
			}

			var popup = currentPopups.Pop();
			popup.Deactivate();
			popup.Hide();

			ActivatePrevPopup();
		}

		private IPopup GetPopup<T>() where T : MonoBehaviour, IPopup
		{
			IPopup popup = popups.FirstOrDefault(x => x is T && x.IsActive == false);
			if (popup == null)
			{
				popup = popupFactory.GetPopup<T>();
				popup.Init();
				popups.Add(popup);
			}

			return popup;
		}

		private void DeactivatePrevPopup()
		{
			if (currentPopups.Count > 0)
			{
				currentPopups.Peek().Deactivate();
			}
		}

		private void ActivatePrevPopup()
		{
			if (currentPopups.Count > 0)
			{
				currentPopups.Peek().Activate();
			}
		}
	}
}

