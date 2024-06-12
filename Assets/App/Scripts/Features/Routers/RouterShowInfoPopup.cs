using Features.Commands;
using Features.Popups.Animations.Animator;
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
		private IPopupAnimator popupAnimator;
		private CloseCommand closeCommand;

		public RouterShowInfoPopup(IPopupController popupController,
							 ILocalizationSystem localizationSystem,
							 IButtonsFactory buttonsFactory,
							 IPopupAnimator popupAnimator,
							 CloseCommand closeCommand)
		{
			this.popupController = popupController;
			this.localizationSystem = localizationSystem;
			this.buttonsFactory = buttonsFactory;
			this.closeCommand = closeCommand;
			this.popupAnimator = popupAnimator;
		}

		public async void ShowInfo(string info)
		{
			InfoPopup popup = popupController.GetPopup<InfoPopup>();

			List<ILabeledCommand> commands = new List<ILabeledCommand>()
			{
				closeCommand,
			};
			GeneralPopupViewModel viewModel = new(info, commands, localizationSystem, buttonsFactory, popupAnimator);
			popup.Setup(viewModel);
			await popup.Show();
		}
	}
}
