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

		private IPlaceholderObject _lastObject = null;

		public AssemblyObjectType Target
		{
			get { return _target; }
		}

		public bool IsBusy
		{
			get
			{
				if (_lastObject != null)
				{
					return _lastObject.Pivot.position == transform.position && _lastObject.Pivot.rotation.eulerAngles == transform.rotation.eulerAngles;
				}

				return false;
			}
		}

		public void SetObject(IPlaceholderObject assemblyObject)
		{
			if (IsBusy || _target != assemblyObject.AssemblyObjectType)
			{
				throw new System.Exception("Invalid object for place");
			}

			_lastObject = assemblyObject;

			_lastObject.Pivot.DOMove(transform.position, _moveToPlaceTime);
			_lastObject.Pivot.DORotate(transform.rotation.eulerAngles, _moveToPlaceTime);
		}
	}
}