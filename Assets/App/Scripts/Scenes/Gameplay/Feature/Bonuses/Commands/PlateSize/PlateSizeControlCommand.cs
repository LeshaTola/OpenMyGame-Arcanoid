using Scenes.Gameplay.Feature.Player;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Bonuses.Commands.PlateSize
{
	public class PlateSizeControlBonusConfig : BonusConfig
	{
		[SerializeField, Min(0)] private float multiplier;
		[SerializeField, Min(0)] private float resizeDuration;

		public float Multiplier { get => multiplier; }
		public float ResizeDuration { get => resizeDuration; }
	}

	public class PlateSizeControlCommand : BonusCommand
	{
		[SerializeField] private PlateSizeControlBonusConfig config;

		private Plate plate;

		public PlateSizeControlCommand(Plate plate, PlateSizeControlBonusConfig config)
		{
			this.plate = plate;
			this.config = config;
		}

		public override BonusConfig Config => config;

		public override void StartBonus()
		{
			base.StartBonus();
			plate.ChangeWidth(config.Multiplier, config.ResizeDuration);
		}

		public override void StopBonus()
		{
			plate.ResetWidth(config.ResizeDuration);
		}
	}
}
