using Features.Bootstrap;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Main.Feature.UI
{
	public class MainMenuUI : MonoBehaviour, IInitializable
	{
		[SerializeField] private Button playButton;
		[SerializeField] private Button continueButton;
		[SerializeField] private Button SwapLanguageButton;

		public event Action OnPlayButtonClicked;
		public event Action OnContinueButtonClicked;
		public event Action OnSwapLanguageButtonClicked;

		public void Init()
		{
			playButton.onClick.AddListener(() => OnPlayButtonClicked?.Invoke());
			continueButton.onClick.AddListener(() => OnContinueButtonClicked?.Invoke());
			SwapLanguageButton.onClick.AddListener(() => OnSwapLanguageButtonClicked?.Invoke());
		}

		public void UpdateUI(bool isContinue)
		{
			continueButton.gameObject.SetActive(isContinue);
		}
	}
}