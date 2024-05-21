namespace Module.Commands
{
	public interface ILabeledCommand
	{
		public string Label { get; }
		public void Execute();
	}
}
