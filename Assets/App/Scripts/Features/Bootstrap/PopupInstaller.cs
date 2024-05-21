using Module.PopupLogic.Configs;
using Module.PopupLogic.General;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Bootstrap
{
	public class PopupInstaller : MonoInstaller
	{
		[SerializeField] private PopupDatabase popupDatabase;

		public override void InstallBindings()
		{
			Container.Bind<IPopupFactory>().To<PopupFactory>().AsSingle().WithArguments(popupDatabase);
			Container.Bind<IPopupController>().To<PopupController>().AsSingle().NonLazy();
		}
	}
}
