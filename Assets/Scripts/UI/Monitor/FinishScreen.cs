using ComputerMaintenanceTraining.Extensions;
using TMPro;
using UnityEngine;

namespace ComputerMaintenanceTraining.UI.Monitor
{
	public class FinishScreen : MonitorScreen
	{
		[SerializeField]
		private TMP_Text _finishText;

		[SerializeField]
		private float _printTextSpeed;

		private Coroutine _printTextCoroutine;

		private const string _text = "Finish";

		private void OnEnable()
		{
			PrintFinishText();
		}

		private void PrintFinishText()
		{
			this.KillCoroutine(ref _printTextCoroutine);
			_printTextCoroutine = StartCoroutine(_finishText.PrintTextCoroutine(_text, _printTextSpeed));
		}
	}
}