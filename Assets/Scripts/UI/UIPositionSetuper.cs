using UnityEngine;

namespace ComputerMaintenanceTraining.UI
{
	public class UIPositionSetuper : MonoBehaviour
	{
		[SerializeField]
		private Transform _camera;

		[SerializeField]
		private Transform _player;

		[SerializeField]
		private float _distance;

		[SerializeField]
		private float _yOffset;

		public void SetupPosition(Transform uiTransform)
		{
			Vector3 playerPosition = _player.position;

			playerPosition.y = _camera.position.y;

			Vector3 yOffset = Vector3.up * _yOffset;

			Vector3 resultPosition = playerPosition + _player.forward * _distance + yOffset;

			uiTransform.position = resultPosition;
		}
	}
}