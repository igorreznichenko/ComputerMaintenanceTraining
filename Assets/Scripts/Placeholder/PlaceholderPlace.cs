using ComputerMaintenanceTraining.Enums;
using DG.Tweening;
using UnityEngine;

namespace ComputerMaintenanceTraining.PlaceholderLogic
{
	public class PlaceholderPlace : MonoBehaviour
	{
		[SerializeField]
		private AssemblyObjectType _target;

		[SerializeField]
		private float _moveToPlaceTime = 1f;

		private IPlaceholderObject _current = null;

		public AssemblyObjectType Target
		{
			get { return _target; }
		}

		public bool IsBusy
		{
			get { return _current != null; }
		}

		public void SetObject(IPlaceholderObject assemblyObject)
		{
			if (IsBusy || _target != assemblyObject.AssemblyObjectType)
			{
				throw new System.Exception("Invalid object for place");
			}

			_current = assemblyObject;

			_current.Pivot.DOMove(transform.position, _moveToPlaceTime);
			_current.Pivot.DORotate(transform.rotation.eulerAngles, _moveToPlaceTime);
		}
	}
}