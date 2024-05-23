using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.PackSelection.Feature.UI
{
	public class HeaderUI : MonoBehaviour, Features.Bootstrap.IInitializable
	{
		[SerializeField] private Button exitButton;

		public event Action OnExitButtonClicked;
		public void Init()
		{
			exitButton.onClick.AddListener(ExitButtonClicked);
		}

		public void ExitButtonClicked()
		{
			OnExitButtonClicked?.Invoke();
		}
	}
}
