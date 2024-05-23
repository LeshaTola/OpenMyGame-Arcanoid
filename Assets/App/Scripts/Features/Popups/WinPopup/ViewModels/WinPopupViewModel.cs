using Features.Saves;
using Module.Commands;
using Scenes.PackSelection.Feature.Packs.Configs;

namespace Features.Popups.WinPopup.ViewModels
{
	public interface IWinPopupViewModel
	{
		ILabeledCommand LoadNextLevelCommand { get; }
		Pack Pack { get; }
		public SavedPackData SavedPackData { get; }

	}

	public class WinPopupViewModel : IWinPopupViewModel
	{
		public WinPopupViewModel(ILabeledCommand loadNextLevelCommand, Pack pack, SavedPackData savedPackData)
		{
			LoadNextLevelCommand = loadNextLevelCommand;
			Pack = pack;
			SavedPackData = savedPackData;
		}

		public ILabeledCommand LoadNextLevelCommand { get; }
		public Pack Pack { get; }
		public SavedPackData SavedPackData { get; }

	}
}
