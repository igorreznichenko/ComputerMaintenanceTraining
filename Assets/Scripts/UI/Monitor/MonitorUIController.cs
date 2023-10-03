using System.Linq;
using UnityEngine;

namespace ComputerMaintenanceTraining.UI
{
	public class MonitorUIController : MonoBehaviour
	{
		[SerializeField]
		private MonitorScreen[] _monitorScreens;

		private MonitorScreen _current = null;

		public void Initialize()
		{
			for (int i = 0; i < _monitorScreens.Length; i++)
			{
				_monitorScreens[i].Init(this);
			}
		}

		public void SwitchScreen<T>() where T : MonitorScreen
		{
			if (_current != null)
			{
				_current.Hide();
			}

			_current = _monitorScreens.First(x => x.GetType() == typeof(T));

			_current.Show();
		}
	}
}