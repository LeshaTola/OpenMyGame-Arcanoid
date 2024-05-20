using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Gameplay.Feature.UI
{
	public class PackInfoUI : MonoBehaviour, IPackInfoUI
	{
		[SerializeField] private Image packImage;
		[SerializeField] private TextMeshProUGUI levelText;

		public void Init(Sprite sprite, int currentLevel, int maxLevel)
		{
			packImage.sprite = sprite;
			levelText.text = $"{currentLevel + 1}/{maxLevel + 1}";
		}
	}
}