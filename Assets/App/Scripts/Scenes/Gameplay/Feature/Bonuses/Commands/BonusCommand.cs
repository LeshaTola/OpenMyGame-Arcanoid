using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Bonuses.Commands
{
	public abstract class BonusCommand : IBonusCommand
	{
		private string id;

		[PreviewField]
		[SerializeField]
		private Sprite sprite;

		[PreviewField]
		[SerializeField]
		private Sprite blockSprite;

		[SerializeField] private float duration;
		[SerializeField] private List<string> conflicts;

		public string Id { get => id; }
		public Sprite Sprite { get => sprite; }
		public Sprite BlockSprite { get => blockSprite; }
		public float Duration { get => duration; }
		public List<string> Conflicts => conflicts;

		public float Timer { get; set; }

		public void Init(string id)
		{
			this.id = id;
		}

		public virtual void Clone(IBonusCommand command)
		{
			id = command.Id;
			sprite = command.Sprite;
			blockSprite = command.BlockSprite;
			duration = command.Duration;
			conflicts = command.Conflicts;
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
