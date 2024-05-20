namespace Module.TimeProvider
{
	public interface ITimeProvider
	{
		public float DeltaTime { get; }
		public float FixedDeltaTime { get; }
	}
}
