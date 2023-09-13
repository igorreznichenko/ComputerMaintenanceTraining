using ComputerMaintenanceTraining.Enums;
using UnityEngine;

namespace ComputerMaintenanceTraining.AssemblyObjects.Detachables
{
	public class DetachedObjectPlace : MonoBehaviour
	{
		[SerializeField]
		private AssemblyObjectType _targetObject;

		[SerializeField]
		private Transform _pivot;

		public AssemblyObjectType TargetObject
		{
			get { return _targetObject; }
		}

		private DetachedObject _current = null;

		public void SetDetachedObject(DetachedObject detachedObject)
		{
			_current = detachedObject;

			detachedObject.Pivot.SetPositionAndRotation(_pivot.position, _pivot.rotation);
		}

		public void Release()
		{
			_current = null;
		}
	}
}