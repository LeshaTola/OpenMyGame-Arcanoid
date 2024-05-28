using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Gameplay.Feature.UI
{
	public class GameplayHeaderUI : MonoBehaviour, Features.Bootstrap.IInitializable
	{
		[SerializeField] private Button menuButton;
		[SerializeField] private Button nextLevelButton;

		public event Action OnMenuButtonCLicked;
		public event Action OnNextLevelButtonCLicked;

		public void Init()
		{
			menuButton.onClick.AddListener(MenuButtonClicked);
			nextLevelButton.onClick.AddListener(NextLevelButtonCLicked);
		}

		private void MenuButtonClicked()
		{
			OnMenuButtonCLicked?.Invoke();
		}

		private void NextLevelButtonCLicked()
		{
			OnNextLevelButtonCLicked?.Invoke();
		}
	}
}