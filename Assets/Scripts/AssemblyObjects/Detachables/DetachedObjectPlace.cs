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

		[SerializeField]
		private bool _allowAttach = true;

		public bool AllowAttach
		{
			get { return _allowAttach; }
			set { _allowAttach = value; }
		}

		public bool CanAttachObject
		{
			get { return Current == null && _allowAttach; }
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
			if (Current != null)
			{
				Current.Pivot.parent = null;
				Current = null;
			}
		}
	}
}