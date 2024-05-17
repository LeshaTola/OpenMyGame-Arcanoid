using Module.PopupLogic.Configs;
using System.Linq;
using UnityEngine;

namespace Module.PopupLogic.General
{
	public class PopupFactory : IPopupFactory
	{
		private PopupDatabase popupDatabase;
		private Transform popupsContainer;

		public PopupFactory(PopupDatabase popupDatabase, Transform popupsContainer)
		{
			this.popupDatabase = popupDatabase;
			this.popupsContainer = popupsContainer;
		}


		public T GetPopup<T>() where T : MonoBehaviour, IPopup
		{
			T popup = (T)popupDatabase.Popups.FirstOrDefault(x => x is T);
			return GameObject.Instantiate(popup, popupsContainer);
		}
	}
}

