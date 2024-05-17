using Module.PopupLogic.Configs;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Module.PopupLogic.General
{
	public class PopupFactory : IPopupFactory
	{
		private PopupDatabase popupDatabase;
		private Transform popupsContainer;
		private DiContainer diContainer;

		public PopupFactory(PopupDatabase popupDatabase, Transform popupsContainer, DiContainer diContainer)
		{
			this.popupDatabase = popupDatabase;
			this.popupsContainer = popupsContainer;
			this.diContainer = diContainer;
		}

		public T GetPopup<T>() where T : MonoBehaviour, IPopup
		{
			T popupTemplate = (T)popupDatabase.Popups.FirstOrDefault(x => x is T);
			if (popupTemplate == null)
			{
				return null;
			}

			T popup = diContainer.InstantiatePrefabForComponent<T>(popupTemplate, popupsContainer);
			return popup;
		}
	}
}

