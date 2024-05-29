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
			Container.Bind<IPopupProvider>().To<PopupProvider>().AsSingle().WithArguments(popupDatabase, popupContainer);
			Container.Bind<IPopupController>().To<PopupController>().AsSingle().WithArguments(screenBlocker).NonLazy();
		}
	}
}
