using SceneReference;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Module.Scenes
{
	public class SceneController : MonoBehaviour
	{
		public void LoadScene(SceneRef scene)
		{
			SceneManager.LoadScene(scene);
		}
	}
}
