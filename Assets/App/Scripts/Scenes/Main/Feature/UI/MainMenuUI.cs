using Features.Bootstrap;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Main.Feature.UI
{
	public class MainMenuUI : MonoBehaviour, IInitializable
	{
		[SerializeField] private Button playButton;
		[SerializeField] private Button SwapLanguageButton;

		public event Action OnPlayButtonClicked;
		public event Action OnSwapLanguageButtonClicked;

		public void Init()
		{
			playButton.onClick.AddListener(() => OnPlayButtonClicked?.Invoke());
			SwapLanguageButton.onClick.AddListener(() => OnSwapLanguageButtonClicked?.Invoke());
		}
	}
}