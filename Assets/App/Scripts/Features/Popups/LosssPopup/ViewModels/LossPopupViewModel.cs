﻿using Module.Commands;
using Module.Localization;

namespace Features.Popups.Loss.ViewModels
{
	public class LossPopupViewModel : ILossPopupViewModel
	{
		public ILabeledCommand RestartCommand { get; }
		public ILabeledCommand ContinueCommand { get; }
		public ILabeledCommand BackCommand { get; }
		public ILocalizationSystem LocalizationSystem { get; }


		public LossPopupViewModel(ILabeledCommand executeRestart,
							ILabeledCommand continueCommand,
							ILabeledCommand backCommand,
							ILocalizationSystem localizationSystem)
		{
			RestartCommand = executeRestart;
			ContinueCommand = continueCommand;
			BackCommand = backCommand;
			LocalizationSystem = localizationSystem;
		}
	}
}