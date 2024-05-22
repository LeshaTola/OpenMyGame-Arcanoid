using Module.PopupLogic.General.Controller;

namespace Module.PopupLogic.General.Popups
{
	public interface IPopup
	{
		public IPopupController Controller { get; }

		public void Show();
		public void Hide();
		public void Activate();
		public void Deactivate();
		void Init(IPopupController controller);
	}
}

