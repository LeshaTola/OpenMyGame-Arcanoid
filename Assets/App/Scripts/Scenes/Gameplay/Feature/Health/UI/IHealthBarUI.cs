namespace Scenes.Gameplay.Feature.Health
{
	public interface IHealthBarUI
	{
		void ActivateAmount(int currentHealth, int prevHealth);
		void CreateUI(int maxHealth);
		void DeactivateAmount(int currentHealth, int prevHealth);
	}
}