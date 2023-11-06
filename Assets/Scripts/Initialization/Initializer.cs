using UnityEngine;

namespace ComputerMaintenanceTraining.Initialization
{
	public class Initializer : MonoBehaviour
	{
		[SerializeField]
		private GameObject[] _initializableObjects;

		public void Initialize()
		{
			IInitializable[] initializables = GetInitializables();

			foreach (var initializable in initializables)
			{
				initializable.Initialize();
			}
		}

		private IInitializable[] GetInitializables()
		{
			IInitializable[] initializables = new IInitializable[_initializableObjects.Length];

			for (int i = 0; i < _initializableObjects.Length; i++)
			{
				initializables[i] = _initializableObjects[i].GetComponent<IInitializable>();
			}

			return initializables;
		}
	}
}