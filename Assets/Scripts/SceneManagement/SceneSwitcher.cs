using ComputerMaintenanceTraining.VisualEffects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ComputerMaintenanceTraining.SceneManagement
{
	public class SceneSwitcher : MonoBehaviour
	{
		[SerializeField]
		private Fader _fader;

		public void SwitchSceneAfterFade(string sceneName)
		{
			_fader.FadeIn(() =>
			{
				SceneManager.LoadScene(sceneName);
			});
		}
	}
}