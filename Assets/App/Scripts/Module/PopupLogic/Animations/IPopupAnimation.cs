using Cysharp.Threading.Tasks;

namespace Module.PopupLogic.General
{
	public interface IPopupAnimation
	{
		public UniTask Show();
		public UniTask Hide();
	}
}
