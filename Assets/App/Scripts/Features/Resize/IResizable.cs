namespace Features.Resize
{
	public interface IResizable
	{
		float SizeMultiplier { get; }

		void Resize(float multiplier);
	}
}
