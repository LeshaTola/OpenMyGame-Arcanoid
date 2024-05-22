using Module.PopupLogic.General.Popups;

namespace Module.PopupLogic.General.Controller
{
	public interface IPopupController
	{
		void HidePopup();
		T GetPopup<T>() where T : Popup;
		void ShowPopup(Popup popup);
		void AddActivePopup(Popup popup);
		void RemoveActivePopup(Popup popup);
	}
}