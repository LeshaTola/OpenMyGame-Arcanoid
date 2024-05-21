using Scenes.Gameplay.StateMachine.States.Loss.Routers;
using Scenes.Gameplay.StateMachine.States.Win.Routers;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Bootstrap
{
	public class RouterInstaller : MonoInstaller
	{
		[SerializeField] private Transform popupContainer;

		public override void InstallBindings()
		{
			CommandInstaller.Install(Container);

			Container.Bind<IRouterShowLoss>().To<RouterShowLoss>().AsSingle().WithArguments(popupContainer);
			Container.Bind<IRouterShowMenu>().To<RouterShowMenu>().AsSingle().WithArguments(popupContainer);
		}
	}
}
