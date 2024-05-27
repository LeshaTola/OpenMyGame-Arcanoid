using Module.Commands;
using Module.PopupLogic.General.Controller;

namespace Features.Commands
{
	public class CloseCommand : ILabeledCommand
	{
		private IPopupController popupController;

		public CloseCommand(IPopupController popupController, string label)
		{
			this.popupController = popupController;
			Label = label;
		}

		public string Label { get; private set; }

		public void Execute()
		{
			popupController.HidePopup();
		}
	}
}
