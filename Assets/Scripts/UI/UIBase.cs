using UnityEngine;

namespace ComputerMaintenanceTraining.UI
{
	public class UIBase : MonoBehaviour
	{
		[SerializeField]
		private Transform _pivot;

		public void Show()
		{
			_pivot.gameObject.SetActive(true);
		}

		public void Hide()
		{
			_pivot.gameObject.SetActive(false);
		}
	}
}