using Features.Energy.Controllers;
using Zenject;

namespace Features.Bootstrap
{
	public class EnergyInstaller : Installer<EnergyInstaller>
	{
		public override void InstallBindings()
		{
			Container.Bind<IEnergyController>().To<EnergyController>().AsSingle();
		}
	}
}
