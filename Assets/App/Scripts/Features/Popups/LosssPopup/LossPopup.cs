using Cysharp.Threading.Tasks;
using Features.Energy.Controllers;
using Features.Energy.UI;
using Features.Popups.Languages;
using Features.Popups.Loss.ViewModels;
using Module.Localization.Localizers;
using Module.PopupLogic.General.Popups;
using System.Collections.Generic;
using TNRD;
using UnityEngine;

namespace Features.Popups.Loss
{
	public class LossPopup : Popup
	{
		[SerializeField] private TMProLocalizer header;

		[SerializeField] private PopupButton restartButton;
		[SerializeField] private PopupButton backButton;
		[SerializeField] private PopupButton addLifeButton;
		[SerializeField] private SerializableInterface<IEnergySliderUI> energySlider;
		[SerializeField] private float animationDuration = 0.15f;

		private ILossPopupViewModel viewModel;
		private EnergyController energyController;

		public void Setup(ILossPopupViewModel viewModel)
		{
			CleanUp();
			Initialize(viewModel);
			SetupLogic(viewModel);
			Translate();

			viewModel.PopupAnimator.Setup(header,
				new List<PopupButton>
					{
						restartButton,
						backButton,
						addLifeButton,
					},
				animationDuration);
			viewModel.PopupAnimator.ResetAnimation();

			energyController = new EnergyController(energySlider.Value, viewModel.EnergyProvider);
			energyController.UpdateUI();
		}

		public async override UniTask Show()
		{
			await base.Show();
			await viewModel.PopupAnimator.ShowAnimation();
		}

		public async override UniTask Hide()
		{
			await viewModel.PopupAnimator.HideAnimation();
			await base.Hide();
		}

		private void SetupLogic(ILossPopupViewModel viewModel)
		{
			restartButton.onButtonClicked += viewModel.RestartCommand.Execute;
			restartButton.UpdateText(viewModel.RestartCommand.Label);

			addLifeButton.onButtonClicked += viewModel.ContinueCommand.Execute;
			addLifeButton.UpdateText(viewModel.ContinueCommand.Label);

			backButton.onButtonClicked += viewModel.BackCommand.Execute;
			backButton.UpdateText(viewModel.BackCommand.Label);
		}

		private void Translate()
		{
			header.Translate();

			restartButton.Translate();
			addLifeButton.Translate();
			backButton.Translate();
		}

		private void Initialize(ILossPopupViewModel viewModel)
		{
			this.viewModel = viewModel;
			header.Init(viewModel.LocalizationSystem);

			restartButton.Init(viewModel.LocalizationSystem);
			addLifeButton.Init(viewModel.LocalizationSystem);
			backButton.Init(viewModel.LocalizationSystem);
		}

		private void CleanUp()
		{
			if (viewModel != null)
			{
				restartButton.onButtonClicked -= viewModel.RestartCommand.Execute;
				addLifeButton.onButtonClicked -= viewModel.ContinueCommand.Execute;
				backButton.onButtonClicked -= viewModel.BackCommand.Execute;
				energyController.CleanUp();
			}
		}
	}
}