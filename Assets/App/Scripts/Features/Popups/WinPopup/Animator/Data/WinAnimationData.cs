using Features.Energy.UI;
using Features.Popups.Languages;
using Features.UI.Animations.SpinAnimation;
using Module.Localization.Localizers;
using TMPro;
using TNRD;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Popups.WinPopup.Animator
{
	public struct WinAnimationData
	{
		public SerializableInterface<IEnergySliderUI> EnergySlider;
		public PopupButton NextButton;
		public SpinAnimation Lines;
		public RectTransform PackImageContainer;
		public Image PackImage;

		public TMProLocalizer Header;
		public TMProLocalizer PackPreNameText;
		public TMProLocalizer PackName;

		public RectTransform LevelLabel;
		public TextMeshProUGUI LevelInfo;

		public float energyAnimationDuration;
		public float imageAnimationDuration;
		public float levelAnimationDuration;
		public float buttonAnimationDuration;

		public int startEnergy;
		public int targetEnergy;
		public int maxEnergy;

		public int targetLevel;
		public int maxLevel;

		public Sprite targetSprite;
		public string targetPackName;
	}
}
