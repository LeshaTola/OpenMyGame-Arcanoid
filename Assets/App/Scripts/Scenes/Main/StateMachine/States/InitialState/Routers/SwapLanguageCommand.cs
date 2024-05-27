using Module.Commands;
using Module.Localization;
using Module.PopupLogic.General.Controller;

namespace Scenes.Main.StateMachine.States.InitialState.Routers
{
	public class SwapLanguageCommand : ILabeledCommand
	{
		private IPopupController popupController;
		private ILocalizationSystem localizationSystem;
		private string languageKey;

		public SwapLanguageCommand(IPopupController popupController, ILocalizationSystem localizationSystem, string languageKey, string label)
		{
			this.popupController = popupController;
			this.localizationSystem = localizationSystem;
			this.languageKey = languageKey;
			Label = label;
		}

		public string Label { get; }

		public void Execute()
		{
			localizationSystem.ChangeLanguage(languageKey);
			popupController.HidePopup();
		}
	}
}
