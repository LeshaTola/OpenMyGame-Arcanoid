using Features.Commands;
using Features.Popups.Languages;
using Module.Commands;
using Module.Localization;
using Module.PopupLogic.General.Controller;
using System.Collections.Generic;

namespace Features.Routers
{
	public class RouterShowInfoPopup : IRouterShowInfoPopup
	{
		private IPopupController popupController;
		private ILocalizationSystem localizationSystem;
		private IButtonsFactory buttonsFactory;
		private CloseCommand closeCommand;

		public RouterShowInfoPopup(IPopupController popupController,
							 ILocalizationSystem localizationSystem,
							 IButtonsFactory buttonsFactory,
							 CloseCommand closeCommand)
		{
			this.popupController = popupController;
			this.localizationSystem = localizationSystem;
			this.buttonsFactory = buttonsFactory;
			this.closeCommand = closeCommand;
		}

		public void ShowInfo(string info)
		{
			InfoPopup popup = popupController.GetPopup<InfoPopup>();

			List<ILabeledCommand> commands = new List<ILabeledCommand>()
			{
				closeCommand,
			};
			GeneralPopupViewModel viewModel = new(info, commands, localizationSystem, buttonsFactory);
			popup.Setup(viewModel);
			popup.Show();
		}
	}
}
