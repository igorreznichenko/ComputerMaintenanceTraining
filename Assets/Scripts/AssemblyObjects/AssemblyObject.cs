using ComputerMaintenanceTraining.Enums;
using UnityEngine;

namespace ComputerMaintenanceTraining.AssemblyObjects
{
	public class AssemblyObject : MonoBehaviour
	{
		[SerializeField]
		private AssemblyObjectType _assemblyObjectType;

		[SerializeField]
		private Transform _pivot;

		public AssemblyObjectType AssemblyObjectType
		{
			get { return _assemblyObjectType; }
		}

		public Transform Pivot
		{
			get { return _pivot; }
		}
	}
}