using Module.PopupLogic.Configs;
using System.Linq;
using UnityEngine;

namespace Module.PopupLogic.General
{
	public class PopupFactory : IPopupFactory
	{
		private PopupDatabase popupDatabase;

		public PopupFactory(PopupDatabase popupDatabase)
		{
			this.popupDatabase = popupDatabase;
		}

		public T GetPopup<T>() where T : MonoBehaviour, IPopup
		{
			T popupTemplate = (T)popupDatabase.Popups.FirstOrDefault(x => x is T);
			if (popupTemplate == null)
			{
				return null;
			}

			T popup = GameObject.Instantiate(popupTemplate);
			return popup;
		}
	}
}

