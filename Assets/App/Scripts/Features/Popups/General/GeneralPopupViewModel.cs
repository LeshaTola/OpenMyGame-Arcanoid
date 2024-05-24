using Module.Commands;
using Module.Localization;
using System.Collections.Generic;

namespace Features.Popups.Languages
{
	public class GeneralPopupViewModel : IGeneralPopupViewModel
	{
		public GeneralPopupViewModel(string header,
							   IEnumerable<ILabeledCommand> commands,
							   ILocalizationSystem localizationSystem,
							   IButtonsFactory buttonsFactory)
		{
			Header = header;
			Commands = commands;
			LocalizationSystem = localizationSystem;
			ButtonsFactory = buttonsFactory;
		}

		public string Header { get; }
		public IEnumerable<ILabeledCommand> Commands { get; }
		public ILocalizationSystem LocalizationSystem { get; }
		public IButtonsFactory ButtonsFactory { get; }

	}
}