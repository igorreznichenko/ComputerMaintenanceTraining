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
		protected Transform _pivot;

		public bool CanAttachObject
		{
			get { return Current != null; }
		}

		public AssemblyObjectType TargetObject
		{
			get { return _targetObject; }
		}

		private DetachedObject _current = null;

		protected DetachedObject Current
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

		public virtual void SetDetachedObject(DetachedObject detachedObject)
		{
			Current = detachedObject;

			detachedObject.Pivot.SetPositionAndRotation(_pivot.position, _pivot.rotation);

			if (detachedObject.Pivot.parent != transform)
			{
				detachedObject.Pivot.parent = transform;
			}
		}

		public virtual void Release()
		{
			Current = null;
		}
	}
}