using Features.Popups.Loss.ViewModels;
using Module.PopupLogic.General.Popups;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Popups.Loss
{
	public class LossPopup : Popup
	{
		[SerializeField] private Button restartButton;
		[SerializeField] private TextMeshProUGUI restartButtonText;

		public void Setup(ILossPopupViewModel popupViewModel)
		{
			restartButton.onClick.RemoveAllListeners();
			restartButton.onClick.AddListener(popupViewModel.RestartCommand.Execute);
			restartButtonText.text = popupViewModel.RestartCommand.Label;
		}
	}
}