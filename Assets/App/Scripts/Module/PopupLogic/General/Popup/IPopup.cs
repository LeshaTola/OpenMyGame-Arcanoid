using Cysharp.Threading.Tasks;
using Module.PopupLogic.General.Controller;

namespace Module.PopupLogic.General.Popups
{
	public interface IPopup
	{
		public IPopupController Controller { get; }

		public UniTask Show();
		public UniTask Hide();
		public void Activate();
		public void Deactivate();
		void Init(IPopupController controller);
	}
}

