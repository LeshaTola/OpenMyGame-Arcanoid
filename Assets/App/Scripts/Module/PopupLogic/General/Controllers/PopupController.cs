using Module.PopupLogic.General.Popups;
using Module.PopupLogic.General.Providers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Module.PopupLogic.General.Controller
{
	public class PopupController : IPopupController
	{
		private IPopupProvider popupProvider;
		private List<Popup> currentPopups;

		public PopupController(IPopupProvider popupProvider)
		{
			this.popupProvider = popupProvider;
			currentPopups = new();
		}

		public void ShowPopup(Popup popup)
		{
			popup.Show();
		}

		public void AddActivePopup(Popup popup)
		{
			DeactivatePrevPopup();
			currentPopups.Add(popup);
		}

		public void RemoveActivePopup(Popup popup)
		{
			if (currentPopups.Count <= 0)
			{
				return;
			}

			if (currentPopups.Last() == popup)
			{
				ActivatePrevPopup();
			}

			currentPopups.Remove(popup);
			popupProvider.PopupPoolsDictionary[popup.GetType()].Release(popup);
		}

		public T GetPopup<T>() where T : Popup
		{
			Type type = typeof(T);
			var popup = popupProvider.PopupPoolsDictionary[type].Get();
			popup.Init(this);
			return (T)popup;
		}

		public void HidePopup()
		{
			if (currentPopups.Count <= 0)
			{
				return;
			}

			var popup = currentPopups.Last();
			popup.Hide();
		}

		private void DeactivatePrevPopup()
		{
			if (currentPopups.Count > 0)
			{
				currentPopups.Last().Deactivate();
				return;
			}
		}

		private void ActivatePrevPopup()
		{
			if (currentPopups.Count > 1)
			{
				currentPopups[currentPopups.Count - 2].Activate();
				return;
			}
		}
	}
}