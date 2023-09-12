using ComputerMaintenanceTraining.VisualEffects;
using UnityEngine;

namespace ComputerMaintenanceTraining.EntryPoints
{
	public class AssemblyEntryPoint : MonoBehaviour
	{
		[SerializeField]
		private Fader _fader;

		private void Awake()
		{
			_fader.FadeOut();
		}
	}
}