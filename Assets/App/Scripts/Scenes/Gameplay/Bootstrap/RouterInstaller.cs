using Scenes.Gameplay.StateMachine.States.Loss;
using UnityEngine;
using Zenject;

namespace Scenes.Gameplay.Bootstrap
{
	public class RouterInstaller : MonoInstaller
	{
		[SerializeField] private Transform popupContainer;

		public override void InstallBindings()
		{
			Container.Bind<IRouterShowLoss>().To<RouterShowLoss>().AsSingle().WithArguments(popupContainer);
		}
	}
}
