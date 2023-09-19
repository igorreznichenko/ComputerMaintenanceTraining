using ComputerMaintenanceTraining.Extensions;
using ComputerMaintenanceTraining.PlaceholderLogic;
using DG.Tweening;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using System;
using System.Collections;
using UnityEngine;
using Tween = DG.Tweening.Tween;

namespace ComputerMaintenanceTraining.AssemblyObjects
{
	public class Screwdriver : AssemblyObject, IPlaceholderObject
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

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out Screwable screwable))
			{
				if (_current != null)
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

			//Set model to bolt place
			float offsetDistance = Vector3.Distance(_modelPivot.position, _screwHeader.position);

			Vector3 targetMovePosition = screwable.ScrewToolPlace.position + screwable.ScrewToolPlace.up * offsetDistance;
			Vector3 targetRotation = screwable.ScrewToolPlace.rotation.eulerAngles;

			_moveToPlaceSequence?.Kill();
			_moveToPlaceSequence = DOTween.Sequence();

			_moveToPlaceSequence.Append(_modelPivot.DOMove(targetMovePosition, _moveToPlaceTime));
			_moveToPlaceSequence.Join(_modelPivot.DORotate(targetRotation, _moveToPlaceTime));

			yield return new WaitUntil(() => _moveToPlaceSequence.IsComplete());

			lastYRotation = screwable.Pivot.TransformWorldToLocalRotation(_modelPivot).eulerAngles.y;

			//look at pivot in one plane

			while (true)
			{
				yield return null;

				Vector3 pivotPosition = Pivot.position;

				pivotPosition = _modelPivot.InverseTransformPoint(pivotPosition);

				pivotPosition.y = 0;

				pivotPosition = _modelPivot.TransformPoint(pivotPosition);

				_modelPivot.LookAt(pivotPosition, _modelPivot.up);

				float currentAngle = screwable.Pivot.TransformWorldToLocalRotation(_modelPivot).eulerAngles.y;

				float difference = currentAngle - lastYRotation;

				//handle difference

				screwable.HandleScrew(difference);

				Vector3 afterMovePosition = screwable.ScrewToolPlace.position + screwable.ScrewToolPlace.up * offsetDistance;
				Quaternion afterMoveRotation = screwable.ScrewToolPlace.rotation;

				_modelPivot.SetPositionAndRotation(afterMovePosition, afterMoveRotation);
			}
		}

		private void ResetModelPivot()
		{
			_modelPivot.parent = Pivot;
			_modelPivot.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
		}
	}
}