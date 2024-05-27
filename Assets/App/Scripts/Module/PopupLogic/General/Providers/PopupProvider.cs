using Module.ObjectPool;
using Module.PopupLogic.Configs;
using Module.PopupLogic.General.Popups;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Module.PopupLogic.General.Providers
{
	public class PopupProvider : IPopupProvider
	{
		public Dictionary<Type, IPool<Popups.Popup>> PopupPoolsDictionary { get; private set; }

		private PopupDatabase popupDatabase;
		private Transform container;

		public PopupProvider(PopupDatabase popupDatabase, Transform container)
		{
			this.popupDatabase = popupDatabase;
			this.container = container;
			Setup(popupDatabase);
		}

		private void Setup(PopupDatabase popupDatabase)
		{
			PopupPoolsDictionary = new();
			foreach (var popup in popupDatabase.Popups)
			{
				var pool = new ObjectPool<Popup>(() => GameObject.Instantiate(popup, container), null, null, 0);
				PopupPoolsDictionary.Add(popup.GetType(), pool);
			}
		}
	}
}