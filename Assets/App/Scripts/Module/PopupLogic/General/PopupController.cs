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

		public T ShowPopup<T>() where T : MonoBehaviour, IPopup
		{
			IPopup popup = GetPopup<T>();

			DeactivatePrevPopup();
			currentPopups.Push(popup);
			popup.Show();

			return (T)popup;
		}

		private IPopup GetPopup<T>() where T : MonoBehaviour, IPopup
		{
			IPopup popup = popups.FirstOrDefault(x => x is T);
			if (popup == null)
			{
				popup = popupFactory.GetPopup<T>();
				if (popup == null)
				{
					return null;
				}
				popups.Add(popup);
			}
			return popup;
		}

		public void HidePopup()
		{
			if (currentPopups.Count <= 0)
			{
				return;
			}

			var popup = currentPopups.Pop();
			popup.Hide();
			ActivatePrevPopup();
		}

		private void DeactivatePrevPopup()
		{
			if (currentPopups.Count > 0)
			{
				currentPopups.Peek().Deactivate();
				return;
			}
		}

		private void ActivatePrevPopup()
		{
			if (currentPopups.Count > 0)
			{
				currentPopups.Peek().Activate();
				return;
			}
		}
	}
}

