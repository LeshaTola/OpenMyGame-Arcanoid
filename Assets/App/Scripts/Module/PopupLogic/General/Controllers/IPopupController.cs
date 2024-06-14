using Cysharp.Threading.Tasks;
using Module.PopupLogic.General.Popups;

namespace Module.PopupLogic.General.Controller
{
	public interface IPopupController
	{
		UniTask HideLastPopup();
		T GetPopup<T>() where T : Popup;
		void AddActivePopup(Popup popup);
		void RemoveActivePopup(Popup popup);
	}
}