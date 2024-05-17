using Module.PopupLogic.Configs;
using Module.PopupLogic.General;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Bootstrap
{
	public class PopupInstaller : MonoInstaller
	{
		[SerializeField] private PopupDatabase popupDatabase;
		[SerializeField] private Transform popupsContainer;

		public override void InstallBindings()
		{
			Container.Bind<IPopupFactory>().To<PopupFactory>().AsSingle().WithArguments(popupDatabase, popupsContainer);
			Container.Bind<IPopupController>().To<PopupController>().AsSingle().NonLazy();
		}
	}
}
