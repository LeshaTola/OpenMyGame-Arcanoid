using Module.ObjectPool;
using System;
using System.Collections.Generic;

namespace Module.PopupLogic.General.Providers
{
	public interface IPopupProvider
	{
		Dictionary<Type, IPool<Popups.Popup>> PopupPoolsDictionary { get; }
	}
}