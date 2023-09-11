using ComputerMaintenanceTraining.UI;
using ComputerMaintenanceTraining.UI.Panels;
using ComputerMaintenanceTraining.VisualEffects;
using UnityEngine;

namespace ComputerMaintenanceTraining.EntryPoints
{
	public class LobbyEntryPoint : MonoBehaviour
	{
		[SerializeField]
		private Fader _fader;

		[SerializeField]
		private Transform _canvas;

		[SerializeField]
		private StartTrainingPanel _startTrainingPanel;

		[SerializeField]
		private UIPositionSetuper _uiPositionSetuper;

		private void Awake()
		{
			_fader.FadeOut();
			_uiPositionSetuper.SetupPosition(_canvas);
			_startTrainingPanel.Show();
		}
	}
}