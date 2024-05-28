using UnityEngine;

namespace Scenes.Gameplay.Feature.Bonuses.Commands
{
	public abstract class BonusCommand : IBonusCommand
	{
		[SerializeField] private int id;
		[SerializeField] private Sprite sprite;
		[SerializeField] private Sprite blockSprite;
		[SerializeField] private float duration;

		public int Id { get => id; }
		public Sprite Sprite { get => sprite; }
		public Sprite BlockSprite { get => blockSprite; }
		public float Duration { get => duration; }

		public float Timer { get; set; }

		public virtual void Clone(IBonusCommand command)
		{
			sprite = command.Sprite;
			blockSprite = command.BlockSprite;
			duration = command.Duration;
		}

		public virtual void StartBonus()
		{
			Timer = Duration;
		}

		public virtual void StopBonus()
		{
		}
	}
}
