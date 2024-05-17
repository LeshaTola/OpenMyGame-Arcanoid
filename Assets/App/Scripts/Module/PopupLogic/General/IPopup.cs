namespace Module.PopupLogic.General
{
	public interface IPopup
	{
		public bool IsActive { get; }
		public void Init();
		public void Show();
		public void Hide();
		public void Activate();
		public void Deactivate();
	}
}

