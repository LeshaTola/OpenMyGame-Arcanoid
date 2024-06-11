using Features.Popups.Animations.Animator;
using Features.Popups.WinPopup.Animator;
using Module.PopupLogic.Configs;
using Module.PopupLogic.General.Controller;
using Module.PopupLogic.General.Providers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scenes.Gameplay.Bootstrap
{
	public class PopupInstaller : MonoInstaller
	{
		[SerializeField] private PopupDatabase popupDatabase;
		[SerializeField] private RectTransform popupContainer;
		[SerializeField] private Image screenBlocker;

		public override void InstallBindings()
		{
			BindPopupAnimator();
			BindPopupProvider();
			BindPopupController();
		}

		private void BindPopupController()
		{
			Container.Bind<IPopupController>().To<PopupController>().AsSingle().WithArguments(screenBlocker).NonLazy();
		}

		private void BindPopupProvider()
		{
			Container.Bind<IPopupProvider>().To<PopupProvider>().AsSingle().WithArguments(popupDatabase, popupContainer);
		}

		private void BindPopupAnimator()
		{
			Container.Bind<IPopupAnimator>().To<GeneralPopupAnimator>().AsTransient();
			Container.Bind<IWinPopupAnimator>().To<WinPopupAnimator>().AsTransient();
		}
	}
}
