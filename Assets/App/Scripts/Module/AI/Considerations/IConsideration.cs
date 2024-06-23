namespace Module.AI.Considerations
{
	public interface IConsideration
	{
		public ConsiderationConfig Config { get; }
		float GetScore();
	}
}
