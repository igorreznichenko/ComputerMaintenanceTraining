using UnityEngine;

namespace ComputerMaintenanceTraining.Extensions
{
	public static class TransformExtensions
	{
		public static void SetLocalZRotation(this Transform transform, float rotation)
		{
			Vector3 localRotation = transform.localEulerAngles;

			localRotation.z = rotation;

			transform.localEulerAngles = localRotation;
		}

	}
}