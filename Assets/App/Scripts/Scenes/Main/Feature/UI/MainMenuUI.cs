using Features.Bootstrap;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Main.Feature.UI
{
	public class MainMenuUI : MonoBehaviour, IInitializable
	{
		[SerializeField] private Button playButton;

		public event Action OnPlayButtonPressed;

		public void Init()
		{
			playButton.onClick.AddListener(() => OnPlayButtonPressed?.Invoke());
		}
	}
}