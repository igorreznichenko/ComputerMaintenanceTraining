using UnityEngine;

namespace ComputerMaintenanceTraining.AssemblyObjects.Detachables
{
	public class ScrewableDetachedObjectPlace : DetachedObjectPlace
	{
		[SerializeField]
		private Transform _screwIn;

		[SerializeField]
		private Transform _screwOut;

		public Transform Pivot
		{
			get { return _pivot; }
		}

		public Transform ScrewIn
		{
			get { return _screwIn; }
		}

		public Transform ScrewOut
		{
			get { return _screwOut; }
		}

		public override void SetDetachedObject(DetachedObject detachedObject)
		{
			ScrewableDetachedObject screwableDetachedObject = (ScrewableDetachedObject)detachedObject;

			Current = detachedObject;

			if (screwableDetachedObject.ScrewInOnAttach)
			{
				detachedObject.Pivot.SetPositionAndRotation(_screwIn.position, _screwIn.rotation);
			}
			else
			{
				detachedObject.Pivot.SetPositionAndRotation(_screwOut.position, _screwOut.rotation);
			}

			if (detachedObject.Pivot.parent != transform)
			{
				detachedObject.Pivot.parent = transform;
			}
		}
	}
}