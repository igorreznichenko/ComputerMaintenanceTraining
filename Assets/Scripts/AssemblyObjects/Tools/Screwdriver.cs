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
	public class Screwdriver : AssemblyObject, ITriggerEnterHandler
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
			_pointableUnityEventWrapper.WhenUnselect.AddListener(OnUnselectEventHandler);
		}

		private void UnsubscribeEvents()
		{
			_pointableUnityEventWrapper.WhenUnselect.RemoveListener(OnUnselectEventHandler);
		}

		private void OnUnselectEventHandler(PointerEvent pointerEvent)
		{
			StopScrewing();
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
			Quaternion lookRotation;

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

			lookRotation = GetCurrentLookRotation();
			lastYRotation = GetYRotationInScrewableLocalSpace(screwable, lookRotation);

			bool stuckInPlace = false;

			while (true)
			{
				yield return null;

				if (!screwable.IsScrewable)
				{
					break;
				}

				lookRotation = GetCurrentLookRotation();

				float currentAngle = GetYRotationInScrewableLocalSpace(screwable, lookRotation);

				float difference = -CircleDegreeUtil.GetMinDegreeDifference(lastYRotation, currentAngle);

				lastYRotation = currentAngle;

				if (difference < 0 && screwable.IsScrewedIn
					|| difference > 0 && screwable.IsScrewedOut)
				{
					stuckInPlace = true;

				}
				else if (stuckInPlace)
				{
					stuckInPlace = false;

					currentAngle = lastYRotation;

				}
				else
				{
					_modelPivot.rotation = lookRotation;

					screwable.HandleScrew(difference);

					Vector3 afterMovePosition = screwable.ScrewToolPlace.position + screwable.ScrewToolPlace.up * offsetDistance;

					_modelPivot.position = afterMovePosition;
				}
			}
		}

		private float GetYRotationInScrewableLocalSpace(Screwable screwable, Quaternion lookRotation)
		{
			return screwable.PlacePivot.TransformWorldToLocalRotation(lookRotation).eulerAngles.y;
		}

		private Quaternion GetCurrentLookRotation()
		{
			Vector3 pivotPosition = Pivot.position;

			pivotPosition = _modelPivot.InverseTransformPoint(pivotPosition);

			pivotPosition.y = 0;

			pivotPosition = _modelPivot.TransformPoint(pivotPosition);

			Vector3 lookRotationDirection = pivotPosition - _modelPivot.position;

			Quaternion lookRotation = Quaternion.LookRotation(lookRotationDirection, _modelPivot.up);

			return lookRotation;
		}

		private void ResetModelPivot()
		{
			_modelPivot.parent = Pivot;
			_modelPivot.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
		}
	}
}