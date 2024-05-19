using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.PackSelection.Feature.Packs.UI
{
	public class PackUI : MonoBehaviour
	{
		[SerializeField] private Button button;
		[SerializeField] private Image image;
		[SerializeField] private TextMeshProUGUI nameText;
		[SerializeField] private TextMeshProUGUI levelsText;

		public void Init(Sprite sprite, string name, int currentLevel, int maxLevel, Action onPackClicked = null)
		{
			button.onClick.AddListener(() => onPackClicked?.Invoke());
			image.sprite = sprite;
			nameText.text = name;
			levelsText.text = (currentLevel + 1) + "/" + (maxLevel + 1);
		}
	}
}
