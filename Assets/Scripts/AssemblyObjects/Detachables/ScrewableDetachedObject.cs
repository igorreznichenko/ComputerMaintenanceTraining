using UnityEngine;

namespace ComputerMaintenanceTraining.AssemblyObjects.Detachables
{
	public class ScrewableDetachedObject : DetachedObject
	{
		[SerializeField]
		private Screwable _screwable;

		[SerializeField]
		private bool _screwInOnStartAttach = true;

		public bool ScrewInOnAttach
		{
			get { return _screwInOnStartAttach; }
		}

		protected override void Start()
		{
			base.Start();
			_screwInOnStartAttach = false;
		}

		protected override void SetToDetachedPlace(DetachedObjectPlace detachedObjectPlace)
		{
			base.SetToDetachedPlace(detachedObjectPlace);

			if (detachedObjectPlace is ScrewableDetachedObjectPlace scarewable)
			{
				_screwable.SetScrewableDetachedObjectPlace(scarewable);
			}
		}


		protected override void ReleaseCurrentPlace()
		{
			base.ReleaseCurrentPlace();
			_screwable.SetScrewableDetachedObjectPlace(null);
		}
	}
}