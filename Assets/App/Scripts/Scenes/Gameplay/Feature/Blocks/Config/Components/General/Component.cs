namespace App.Scripts.Scenes.Gameplay.Feature.Blocks.Config.Components.General
{
	public abstract class Component : IComponent
	{
		protected Block Block;

		public virtual void Init(Block block)
		{
			Block = block;
		}

		public virtual void Execute()
		{
		}


	}
}
