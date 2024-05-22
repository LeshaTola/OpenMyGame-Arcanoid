using Features.Popups.Menu.ViewModels;
using Module.PopupLogic.General.Popups;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Popups.Menu
{
	public class MenuPopup : Popup
	{
		[SerializeField] private Button restartButton;
		[SerializeField] private Button backButton;
		[SerializeField] private Button resumeButton;

		[SerializeField] private TextMeshProUGUI restartButtonText;
		[SerializeField] private TextMeshProUGUI backButtonText;
		[SerializeField] private TextMeshProUGUI resumeButtonText;

		public void Setup(IMenuPopupViewModel menuPopupViewModel)
		{
			CleanUp();
			restartButton.onClick.AddListener(menuPopupViewModel.RestartCommand.Execute);
			backButton.onClick.AddListener(menuPopupViewModel.BackCommand.Execute);
			resumeButton.onClick.AddListener(menuPopupViewModel.ResumeCommand.Execute);

			restartButtonText.text = menuPopupViewModel.RestartCommand.Label;
			backButtonText.text = menuPopupViewModel.BackCommand.Label;
			resumeButtonText.text = menuPopupViewModel.ResumeCommand.Label;
		}

		private void CleanUp()
		{
			restartButton.onClick.RemoveAllListeners();
			backButton.onClick.RemoveAllListeners();
			resumeButton.onClick.RemoveAllListeners();
		}
	}
}