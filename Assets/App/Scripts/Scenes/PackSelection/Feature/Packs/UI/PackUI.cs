using Features.Saves;
using Module.Localization;
using Module.Localization.Localizers;
using Scenes.PackSelection.Feature.Packs.Configs;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.PackSelection.Feature.Packs.UI
{
	public class PackUI : MonoBehaviour
	{
		[SerializeField] private PackAppearanceTypes appearanceTypes;

		[SerializeField] private Button button;

		[SerializeField] private Image Background;
		[SerializeField] private Image PackImageBackground;
		[SerializeField] private Image packImage;

		[SerializeField] private TMProLocalizer headerNameText;
		[SerializeField] private TMProLocalizer packNameText;
		[SerializeField] private TextMeshProUGUI levelsText;

		public event Action onPackClicked;

		public void UpdateUI(Pack pack, SavedPackData savedPackData, ILocalizationSystem localizationSystem)
		{
			Initialize(localizationSystem);

			levelsText.text = (savedPackData.CurrentLevel) + "/" + (pack.MaxLevel + 1);

			if (!savedPackData.IsOpened)
			{
				SwapAppearance(appearanceTypes.Closed);
				Translate();
				return;
			}

			if (savedPackData.IsCompeted)
			{
				SwapAppearance(appearanceTypes.Complete);
			}
			else
			{
				SwapAppearance(appearanceTypes.InProgress);
			}

			button.onClick.AddListener(() => onPackClicked?.Invoke());

			packImage.sprite = pack.Sprite;
			packNameText.Key = pack.Name;

			Translate();
		}

		private void Initialize(ILocalizationSystem localizationSystem)
		{
			packNameText.Init(localizationSystem);
			headerNameText.Init(localizationSystem);
		}

		private void Translate()
		{
			packNameText.Translate();
			headerNameText.Translate();
		}

		public void SwapAppearance(PackAppearance packAppearance)
		{
			Background.sprite = packAppearance.Background;
			PackImageBackground.sprite = packAppearance.PackImageBackground;
			headerNameText.Text.color = packAppearance.HeaderColor;
			packNameText.Text.color = packAppearance.NameColor;
			levelsText.color = packAppearance.LevelColor;
		}

	}

	[Serializable]
	public struct PackAppearanceTypes
	{
		public PackAppearance InProgress;
		public PackAppearance Complete;
		public PackAppearance Closed;
	}

	[Serializable]
	public class PackAppearance
	{
		public Sprite Background;
		public Sprite PackImageBackground;
		public Color HeaderColor = Color.white;
		public Color NameColor = Color.white;
		public Color LevelColor = Color.white;
	}
}
