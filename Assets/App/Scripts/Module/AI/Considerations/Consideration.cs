namespace Module.AI.Considerations
{
	public abstract class Consideration : IConsideration
	{

		public abstract ConsiderationConfig Config { get; }
		public abstract float GetScore();
	}

	public class ConsiderationConfig
	{

	}

}
