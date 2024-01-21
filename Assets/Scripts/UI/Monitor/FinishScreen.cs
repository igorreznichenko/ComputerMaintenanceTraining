using ComputerMaintenanceTraining.Constants;
using ComputerMaintenanceTraining.Extensions;
using ComputerMaintenanceTraining.SceneManagement;
using System.Collections;
using TMPro;
using UnityEngine;

namespace ComputerMaintenanceTraining.UI.Monitor
{
	public class FinishScreen : MonitorScreen
	{
		[SerializeField]
		private TMP_Text _finishText;

		[SerializeField]
		private TMP_Text _restartInfo;

		[SerializeField]
		private SceneSwitcher _sceneLoader;

		[SerializeField]
		private int _secondsToRestart;

		[SerializeField]
		private float _printTextSpeed;

		private Coroutine _printTextCoroutine;

		private const string _text = "Finish";

		private const string _restartTextTemplate = "Restart in {0} seconds";

		private void OnEnable()
		{
			PrintFinishText();
		}

		private void PrintFinishText()
		{
			this.KillCoroutine(ref _printTextCoroutine);
			_printTextCoroutine = StartCoroutine(PrintFinishTextCoroutine());
		}

		private IEnumerator PrintFinishTextCoroutine()
		{
			int currentTimeToRestart = _secondsToRestart;

			yield return _finishText.PrintTextCoroutine(_text, _printTextSpeed);

			yield return _restartInfo.PrintTextCoroutine(string.Format(_restartTextTemplate, currentTimeToRestart), _printTextSpeed);

			while (currentTimeToRestart > 0)
			{
				yield return new WaitForSeconds(1);

				currentTimeToRestart--;
				_restartInfo.text = string.Format(_restartTextTemplate, currentTimeToRestart);
			}

			_sceneLoader.SwitchSceneAfterFade(SceneNames.ASSEMBLY_SCENE_NAME);
		}
	}
}