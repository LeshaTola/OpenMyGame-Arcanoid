namespace Scenes.Gameplay.Feature.Bonuses.Commands
{
	public interface IBonusCommand
	{
		string Id { get; }
		float Timer { get; set; }
		BonusConfig Config { get; }

		public void Init(string Id);
		void StartBonus();
		void StopBonus();
	}
}
