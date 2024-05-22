using Module.Commands;
using Scenes.PackSelection.Feature.Packs.Configs;

namespace Features.Popups.WinPopup.ViewModels
{
	public interface IWinPopupViewModel
	{
		ILabeledCommand LoadNextLevelCommand { get; }
		Pack Pack { get; }
	}

	public class WinPopupViewModel : IWinPopupViewModel
	{
		public WinPopupViewModel(ILabeledCommand loadNextLevelCommand, Pack pack)
		{
			LoadNextLevelCommand = loadNextLevelCommand;
			Pack = pack;
		}

		public ILabeledCommand LoadNextLevelCommand { get; }
		public Pack Pack { get; }

	}
}
