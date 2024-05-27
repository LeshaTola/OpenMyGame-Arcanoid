using Scenes.Gameplay.Feature.Blocks;
using Scenes.Gameplay.Feature.Blocks.Config.Components.Health;
using Scenes.Gameplay.Feature.Blocks.Config.Components.Score;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scenes.Gameplay.Feature.Progress
{
	public class ProgressController : IProgressController
	{
		public event Action OnWin;

		private IProgressUI progressUI;
		private int winProgress;

		private List<Block> scoredBlocks;
		private int startBlocksCount;
		private int currentBlocksCount;

		public ProgressController(IProgressUI progressUI, int winProgress)
		{
			this.progressUI = progressUI;
			this.winProgress = winProgress;
		}

		public int Progress
		{
			get => (int)(NormalizedProgress * 100);
		}

		public float NormalizedProgress
		{
			get => 1f - ((float)currentBlocksCount / startBlocksCount);
		}

		public void Init(List<Block> blocks)
		{
			scoredBlocks = blocks.Where(x => x.Config.GetComponent<ScoreComponent>() != null).ToList();

			foreach (var block in scoredBlocks)
			{
				SubscribeOnBlock(block);
			}

			startBlocksCount = scoredBlocks.Count;
			currentBlocksCount = startBlocksCount;

			ProcessProgress();
		}

		public void ProcessProgress()
		{
			progressUI.UpdateProgress(Progress);

			if (Progress == winProgress)
			{
				OnWin?.Invoke();
			}
		}

		public void CleanUp()
		{
			foreach (var block in scoredBlocks)
			{
				UnsubscribeFromBlock(block);
			}
			scoredBlocks.Clear();

			startBlocksCount = 1;
			currentBlocksCount = startBlocksCount;

			ProcessProgress();
		}

		private void SubscribeOnBlock(Block block)
		{
			var healthComponent = block.Config.GetComponent<HealthComponent>();
			if (healthComponent != null)
			{
				healthComponent.OnDeath += OnBlockDeath;
			}
		}

		private void UnsubscribeFromBlock(Block block)
		{
			var healthComponent = block.Config.GetComponent<HealthComponent>();
			if (healthComponent != null)
			{
				healthComponent.OnDeath -= OnBlockDeath;
			}
		}

		private void OnBlockDeath(Block block)
		{
			scoredBlocks.Remove(block);

			UnsubscribeFromBlock(block);

			currentBlocksCount--;
			ProcessProgress();
		}


	}
}
