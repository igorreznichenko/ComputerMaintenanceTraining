using ComputerMaintenanceTraining.Enums;
using System;
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

		private DetachedObject Current
		{
			get { return _current; }
			set
			{
				if (_current != value)
				{
					_current = value;

					OnDetachedObjectChanged?.Invoke(value);
				}
			}
		}

		public event Action<DetachedObject> OnDetachedObjectChanged = default;

		public void SetDetachedObject(DetachedObject detachedObject)
		{
			Current = detachedObject;

			detachedObject.Pivot.SetPositionAndRotation(_pivot.position, _pivot.rotation);

			if (detachedObject.Pivot.parent != transform)
			{
				detachedObject.Pivot.parent = transform;
			}
		}

		public void Release()
		{
			Current = null;
		}
	}
}