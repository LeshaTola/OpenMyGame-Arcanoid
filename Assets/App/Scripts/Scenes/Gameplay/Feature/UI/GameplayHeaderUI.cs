using Features.Popups;
using Module.PopupLogic.General;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scenes.Gameplay.Feature.UI
{
	public class GameplayHeaderUI : MonoBehaviour, Features.Bootstrap.IInitializable
	{
		[SerializeField] private Button mainMenuButton;

		private IPopupController popupController;

		[Inject]
		public void Construct(IPopupController popupController)
		{
			this.popupController = popupController;
			popupController.OnFirstPopupActivates += OnFirstPopupActivates;
			popupController.OnLastPopupDeactivates += OnLastPopupDeactivates;
		}

		private void OnDestroy()
		{
			popupController.OnFirstPopupActivates -= OnFirstPopupActivates;
			popupController.OnLastPopupDeactivates -= OnLastPopupDeactivates;
		}

		public void Init()
		{
			mainMenuButton.onClick.AddListener(MenuButtonClicked);
		}

		public void MenuButtonClicked()
		{
			popupController.ShowPopup<MenuPopup>();
		}

		private void Activate()
		{
			mainMenuButton.enabled = true;
		}

		private void Deactivate()
		{
			mainMenuButton.enabled = false;
		}

		private void OnLastPopupDeactivates()
		{
			Activate();
		}

		private void OnFirstPopupActivates()
		{
			Deactivate();
		}
	}
}