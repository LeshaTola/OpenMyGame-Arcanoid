using Cysharp.Threading.Tasks;
using Module.PopupLogic.General.Popups;

namespace Module.PopupLogic.General.Controller
{
	public interface IPopupController
	{
		UniTask HidePopup();
		T GetPopup<T>() where T : Popup;
		UniTask ShowPopup(Popup popup);
		void AddActivePopup(Popup popup);
		void RemoveActivePopup(Popup popup);
	}
}