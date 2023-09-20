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

		[SerializeField]
		private GameObject _currentObject;

		private IPlaceholderObject _current = null;

		private IPlaceholderObject Current
		{
			set
			{
				if (_current != null)
				{
					if (value == null)
					{
						_current.OnPlaceholderPlaceChangedEventHandler(null);
					}

				}
				else if (value != null)
				{
					value.OnPlaceholderPlaceChangedEventHandler(this);
				}

				_current = value;
			}
		}

		private Sequence _moveToPlace;

		private bool _isStartMoveToPlace = false;

		public AssemblyObjectType Target
		{
			get { return _target; }
		}

		public bool IsBusy
		{
			get
			{
				return _current != null;
			}
		}

		private void Start()
		{
			if (_currentObject != null &&
				_currentObject.TryGetComponent(out IPlaceholderObject placeholderObject))
			{
				SetObject(placeholderObject);
			}
		}

		public void SetObject(IPlaceholderObject assemblyObject)
		{
			if (IsBusy || _target != assemblyObject.AssemblyObjectType)
			{
				throw new System.Exception("Invalid object for place");
			}

			_moveToPlace?.Kill();

			Current = assemblyObject;

			bool alreadyOnPosition = IsTransformOnPlace(assemblyObject.Pivot);

			if (!alreadyOnPosition)
			{
				_moveToPlace = DOTween.Sequence();
				_isStartMoveToPlace = true;

				_moveToPlace.Join(_current.Pivot.DOMove(transform.position, _moveToPlaceTime));
				_moveToPlace.Join(_current.Pivot.DORotate(transform.rotation.eulerAngles, _moveToPlaceTime));

				_moveToPlace.AppendCallback((() => 
				_isStartMoveToPlace = false
				));
			}
		}

		private void Update()
		{
			CheckCurrentObjectOnPlace();
		}

		private void CheckCurrentObjectOnPlace()
		{
			if (_current == null || _isStartMoveToPlace)
				return;


			bool isInRightPosition = IsTransformOnPlace(_current.Pivot);

			if (!isInRightPosition)
			{
				Current = null;
			}
		}

		private bool IsTransformOnPlace(Transform checkedTransform)
		{
			return checkedTransform.position == transform.position && checkedTransform.rotation.eulerAngles == transform.rotation.eulerAngles;
		}
	}
}