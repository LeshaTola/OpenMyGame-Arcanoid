using System;

namespace Module.PopupLogic.General
{
	public interface IPopupAnimation
	{
		public void Show(Action onComplete = null);
		public void Hide(Action onComplete = null);
	}
}
