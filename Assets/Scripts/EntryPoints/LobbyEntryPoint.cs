using ComputerMaintenanceTraining.VisualEffects;
using UnityEngine;

namespace ComputerMaintenanceTraining.EntryPoints
{
	public class LobbyEntryPoint : MonoBehaviour
	{
		[SerializeField]
		private Fader _fader;

		private void Awake()
		{
			_fader.FadeOut();
		}
	}
}