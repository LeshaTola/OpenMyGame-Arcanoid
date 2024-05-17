using System;

namespace Module.PopupLogic.General
{
	public interface IPopupAnimation
	{
		public void Show(Action onComplete);
		public void Hide(Action onComplete);
	}
}
