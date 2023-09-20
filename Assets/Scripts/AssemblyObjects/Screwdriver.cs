using ComputerMaintenanceTraining.Collisions;
using ComputerMaintenanceTraining.Extensions;
using ComputerMaintenanceTraining.PlaceholderLogic;
using ComputerMaintenanceTraining.Utils;
using DG.Tweening;
using Oculus.Interaction;
using System;
using System.Collections;
using UnityEngine;

namespace ComputerMaintenanceTraining.AssemblyObjects
{
	public class Screwdriver : AssemblyObject, IPlaceholderObject, ITriggerable
	{
		[SerializeField]
		private Transform _modelPivot;

		public Transform ModelPivot
		{
			get { return _modelPivot; }
		}

		[SerializeField]
		private Transform _screwHeader;

		public Transform ScrewHeader
		{
			get { return _screwHeader; }
		}

		[SerializeField]
		private PointableUnityEventWrapper _pointableUnityEventWrapper;

		private Screwable _current = null;

		private Coroutine _screwingCoroutine = null;

		private Sequence _moveToPlaceSequence = null;

		private float _moveToPlaceTime = 0.5f;

		#region placeholder

		private bool _canBePlacedToPlaceholder = false;

		public bool CanBePlacedToPlaceholder
		{
			get { return _canBePlacedToPlaceholder; }
			set
			{
				if (_canBePlacedToPlaceholder != value)
				{
					_canBePlacedToPlaceholder = value;
					OnPlacableStateChanged?.Invoke(this);
				}
			}
		}

		public event Action<IPlaceholderObject> OnPlacableStateChanged;
		public event Action<PlaceholderPlace> OnPlaceholderPlaceChanged;

		#endregion

		private void OnEnable()
		{
			SubscribeEvents();
		}

		private void OnDisable()
		{
			UnsubscribeEvents();
		}

		private void SubscribeEvents()
		{
			_pointableUnityEventWrapper.WhenSelect.AddListener(OnSelectEventHandler);
			_pointableUnityEventWrapper.WhenUnselect.AddListener(OnUnselectEventHandler);
		}

		private void UnsubscribeEvents()
		{
			_pointableUnityEventWrapper.WhenSelect.RemoveListener(OnSelectEventHandler);
			_pointableUnityEventWrapper.WhenUnselect.RemoveListener(OnUnselectEventHandler);
		}

		private void OnSelectEventHandler(PointerEvent pointerEvent)
		{
			CanBePlacedToPlaceholder = false;
		}

		private void OnUnselectEventHandler(PointerEvent pointerEvent)
		{
			StopScrewing();

			CanBePlacedToPlaceholder = true;
		}

		public void OnPlaceholderPlaceChangedEventHandler(PlaceholderPlace placeholderPlace)
		{
			OnPlaceholderPlaceChanged?.Invoke(placeholderPlace);
		}

		public void TriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out Screwable screwable))
			{
				if (_current != null || !screwable.IsScrewable)
				{
					return;
				}

				StartScrewing(screwable);
			}
		}

		public void TriggerExit(Collider other) { }

		private void StartScrewing(Screwable screwable)
		{
			this.KillCoroutine(ref _screwingCoroutine);
			_current = screwable;
			_modelPivot.parent = null;

			_screwingCoroutine = StartCoroutine(ScrewCoroutine(screwable));
		}

		private void StopScrewing()
		{
			if (_current != null)
			{
				_moveToPlaceSequence?.Kill();
				this.KillCoroutine(ref _screwingCoroutine);
				Pivot.position = _modelPivot.position;
				ResetModelPivot();
				_current = null;
			}
		}

		private IEnumerator ScrewCoroutine(Screwable screwable)
		{
			float lastYRotation;

			//Set model to bolt place
			float offsetDistance = Vector3.Distance(_modelPivot.position, _screwHeader.position);

			Vector3 targetMovePosition = screwable.ScrewToolPlace.position + screwable.ScrewToolPlace.up * offsetDistance;
			Vector3 targetRotation = screwable.ScrewToolPlace.rotation.eulerAngles;

			_moveToPlaceSequence?.Kill();
			_moveToPlaceSequence = DOTween.Sequence();

			_moveToPlaceSequence.Append(_modelPivot.DOMove(targetMovePosition, _moveToPlaceTime));
			_moveToPlaceSequence.Join(_modelPivot.DORotate(targetRotation, _moveToPlaceTime));

			bool isOnPlace = false;

			_moveToPlaceSequence.OnComplete(() => isOnPlace = true);

			yield return new WaitUntil(() => isOnPlace);

			lastYRotation = screwable.Pivot.TransformWorldToLocalRotation(_modelPivot.rotation).eulerAngles.y;

			//look at pivot in one plane

			bool stuckInPlace = false;

			while (true)
			{
				yield return null;

				if(!screwable.IsScrewable)
				{
					break;
				}

				Vector3 pivotPosition = Pivot.position;

				pivotPosition = _modelPivot.InverseTransformPoint(pivotPosition);

				pivotPosition.y = 0;

				pivotPosition = _modelPivot.TransformPoint(pivotPosition);

				Vector3 lookRotationDirection = pivotPosition - _modelPivot.position;

				Quaternion lookRotation = Quaternion.LookRotation(lookRotationDirection, _modelPivot.up);

				float currentAngle = screwable.PlacePivot.TransformWorldToLocalRotation(lookRotation).eulerAngles.y;

				float difference =  CircleDegreeUtil.GetMinDegreeDifference(lastYRotation, currentAngle);

				lastYRotation = currentAngle;

				print("IsScrewedIn in screwdriver" + screwable.IsScrewedIn);


				if (difference < 0 && screwable.IsScrewedIn
					|| difference > 0 && screwable.IsScrewedOut)
				{
					stuckInPlace = true;

					print("Stuck");
				}
				else if (stuckInPlace)
				{
					stuckInPlace = false;

					currentAngle = lastYRotation;
					print("Stop stuck");

				}
				else
				{
					////handle difference

					print("Handle");

					_modelPivot.rotation = lookRotation;

					screwable.HandleScrew(difference);

					Vector3 afterMovePosition = screwable.ScrewToolPlace.position + screwable.ScrewToolPlace.up * offsetDistance;

					_modelPivot.position = afterMovePosition;
				}
			}
		}

		private void ResetModelPivot()
		{
			_modelPivot.parent = Pivot;
			_modelPivot.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
		}
	}
}