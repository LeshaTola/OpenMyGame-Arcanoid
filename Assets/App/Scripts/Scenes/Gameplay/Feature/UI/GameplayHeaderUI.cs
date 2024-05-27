using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Gameplay.Feature.UI
{
	public class GameplayHeaderUI : MonoBehaviour, Features.Bootstrap.IInitializable
	{
		[SerializeField] private Button menuButton;

		public event Action OnMenuButtonCLicked;

		public void Init()
		{
			menuButton.onClick.AddListener(MenuButtonClicked);
		}

		public void MenuButtonClicked()
		{
			OnMenuButtonCLicked?.Invoke();
		}
	}
}