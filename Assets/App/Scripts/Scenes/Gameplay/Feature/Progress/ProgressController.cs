using Scenes.Gameplay.Feature.Blocks;
using Scenes.Gameplay.Feature.Blocks.Config.Components.Health;
using Scenes.Gameplay.Feature.Blocks.Config.Components.Score;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scenes.Gameplay.Feature.Progress
{
	public class ProgressController : MonoBehaviour, IProgressController
	{
		[SerializeField] private ProgressUI progressUI;

		private List<Block> scoredBlocks;
		private int startBlocksCount;
		private int currentBlocksCount;

		public void Init(List<Block> blocks)
		{
			scoredBlocks = blocks.Where(x => x.Config.GetComponent<ScoreComponent>() != null).ToList();

			foreach (var block in scoredBlocks)
			{
				var healthComponent = block.Config.GetComponent<HealthComponent>();
				if (healthComponent != null)
				{
					healthComponent.OnDeath += OnBlockDeath;
				}
			}

			startBlocksCount = blocks.Count;
			currentBlocksCount = startBlocksCount;

			ProcessProgress();
		}

		private void OnBlockDeath(Block block)
		{
			scoredBlocks.Remove(block);

			var healthComponent = block.Config.GetComponent<HealthComponent>();
			if (healthComponent != null)
			{
				healthComponent.OnDeath += OnBlockDeath;
			}

			currentBlocksCount--;
			ProcessProgress();
		}

		public void ProcessProgress()
		{
			int progress = CalculateProgress();
			progressUI.UpdateProgress(progress);

			if (progress > 0)
			{
				//TODO: popup manager show win popup
			}
		}

		public int CalculateProgress()
		{
			return (int)((1f - ((float)currentBlocksCount / startBlocksCount)) * 100);
		}
	}
}
