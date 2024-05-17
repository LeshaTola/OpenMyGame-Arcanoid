using SceneReference;
using UnityEngine.SceneManagement;

namespace Module.Scenes
{
	public class SceneLoadService : ISceneLoadService
	{
		public void LoadScene(SceneRef scene)
		{
			SceneManager.LoadScene(scene);
		}
	}
}
