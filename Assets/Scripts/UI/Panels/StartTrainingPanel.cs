using ComputerMaintenanceTraining.Constants;
using ComputerMaintenanceTraining.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace ComputerMaintenanceTraining.UI.Panels
{
	public class StartTrainingPanel : UIBase
	{
		[SerializeField]
		private Button _startTraining;

		[SerializeField]
		private SceneSwitcher _sceneSwitcher;

		private void OnEnable()
		{
			SubscribeEvents();
		}

		private void OnDisable()
		{
			UnsubscribeEvents();
		}

		private void SubscribeEvents()
		{
			_startTraining.onClick.AddListener(OnStartTrainingButtonClickHandler);
		}

		private void UnsubscribeEvents()
		{
			_startTraining.onClick.RemoveListener(OnStartTrainingButtonClickHandler);
		}

		private void OnStartTrainingButtonClickHandler()
		{
			_sceneSwitcher.SwitchSceneAfterFade(SceneNames.ASSEMBLY_SCENE_NAME);
		}
	}
}