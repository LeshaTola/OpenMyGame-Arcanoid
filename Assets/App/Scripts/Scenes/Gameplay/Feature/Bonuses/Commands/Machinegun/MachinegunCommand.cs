using Scenes.Gameplay.Feature.Player.Machineguns;
using Scenes.Gameplay.Feature.Player.Machineguns.Bullets;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Bonuses.Commands.Machineguns
{
	public class MachinegunCommand : BonusCommand
	{
		[SerializeField] private float timeBetweenShots;
		[SerializeField] private BulletParams bulletParams;

		private Machinegun machinegun;

		public MachinegunCommand(Machinegun machinegun)
		{
			this.machinegun = machinegun;
		}

		public override void Clone(IBonusCommand command)
		{
			base.Clone(command);
			MachinegunCommand concreteCommand = (MachinegunCommand)command;
			timeBetweenShots = concreteCommand.timeBetweenShots;
			bulletParams = concreteCommand.bulletParams;
		}

		public override void StartBonus()
		{
			base.StartBonus();
			machinegun.IsActive = true;
			machinegun.Setup(timeBetweenShots, bulletParams);
		}

		public override void StopBonus()
		{
			base.StopBonus();
			machinegun.IsActive = false;
		}
	}
}
