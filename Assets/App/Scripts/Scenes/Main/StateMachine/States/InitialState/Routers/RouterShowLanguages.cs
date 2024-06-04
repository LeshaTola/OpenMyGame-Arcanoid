using Features.Popups.Languages;
using Module.Commands;
using Module.Localization;
using Module.PopupLogic.General.Controller;
using System.Collections.Generic;

namespace Scenes.Main.StateMachine.States.InitialState.Routers
{
	public class RouterShowLanguages : IRouterShowLanguages
	{
		private IPopupController popupController;
		private ILocalizationSystem localizationSystem;
		private IButtonsFactory buttonsFactory;

		public RouterShowLanguages(IPopupController popupController,
							 ILocalizationSystem localizationSystem,
							 IButtonsFactory buttonsFactory)
		{
			this.popupController = popupController;
			this.localizationSystem = localizationSystem;
			this.buttonsFactory = buttonsFactory;
		}

		public async void ShowLanguages()
		{
			var languagesPopup = popupController.GetPopup<LanguagesPopup>();
			IEnumerable<ILabeledCommand> commands = FormCommands();
			IGeneralPopupViewModel viewModel = new GeneralPopupViewModel("languages", commands, localizationSystem, buttonsFactory);
			languagesPopup.Setup(viewModel);
			await languagesPopup.Show();

		}

		private IEnumerable<ILabeledCommand> FormCommands()
		{
			var commands = new List<ILabeledCommand>();
			foreach (var language in localizationSystem.GetLanguages())
			{
				var command = new SwapLanguageCommand(popupController, localizationSystem, language, language);
				commands.Add(command);
			}
			return commands;
		}
	}
}
