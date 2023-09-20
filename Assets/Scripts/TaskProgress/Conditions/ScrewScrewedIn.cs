using ComputerMaintenanceTraining.AssemblyObjects;
using UnityEngine;

namespace ComputerMaintenanceTraining.TaskProgress.Conditions
{
	public class ScrewScrewedIn : Condition
	{
		[SerializeField]
		private Screwable _target;

		private void OnEnable()
		{
			SubscribeEvents();
		}

		private void OnDisable()
		{
			UnsubdcribeEvents();
		}

		private void SubscribeEvents()
		{
			_target.OnScrewedIn += OnScrewScrewedIn;
			_target.OnScrewedOut += OnScrewScrewedOut;
			_target.OnTransitedToScrew += OnTransitionedToScrew;
		}

		private void UnsubdcribeEvents()
		{
			_target.OnScrewedIn += OnScrewScrewedIn;
			_target.OnScrewedOut += OnScrewScrewedOut;
			_target.OnTransitedToScrew += OnTransitionedToScrew;

		}

		private void OnTransitionedToScrew()
		{
			Satisfied = false;
		}


		private void OnScrewScrewedOut()
		{
			Satisfied = false;
		}

		private void OnScrewScrewedIn()
		{
			Satisfied = true;
		}
	}
}