using Scenes.Gameplay.Feature.Player.Machineguns;
using Scenes.Gameplay.Feature.Player.Machineguns.Bullets;
using System;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Bonuses.Commands.Machineguns
{
	[Serializable]
	public class MachinegunBonusConfig : BonusConfig
	{
		[SerializeField] private float timeBetweenShots;
		[SerializeField] private BulletParams bulletParams;

		public float TimeBetweenShots { get => timeBetweenShots; }
		public BulletParams BulletParams { get => bulletParams; }
	}

	public class MachinegunCommand : BonusCommand
	{
		[SerializeField] private MachinegunBonusConfig config;

		private Machinegun machinegun;

		public MachinegunCommand(Machinegun machinegun, MachinegunBonusConfig config)
		{
			this.machinegun = machinegun;
			this.config = config;
		}

		public override BonusConfig Config => config;

		public override void StartBonus()
		{
			base.StartBonus();
			machinegun.IsActive = true;
			machinegun.Setup(config.TimeBetweenShots, config.BulletParams);
		}

		public override void StopBonus()
		{
			base.StopBonus();
			machinegun.IsActive = false;
		}
	}
}
