using ComputerMaintenanceTraining.Enums;
using UnityEngine;

namespace ComputerMaintenanceTraining.Extensions
{
	public static class TransformExtensions
	{
		public static void SetLocalRotationForAxis(this Transform transform, float rotation, Axis axis)
		{
			Vector3 localRotation = transform.localEulerAngles;

			localRotation.z = rotation;

			switch (axis)
			{
				case Axis.None:
					return;

				case Axis.X:
					localRotation.x = rotation;
					break;

				case Axis.Y:
					localRotation.y = rotation;
					break;

				case Axis.Z:
					localRotation.z = rotation;
					break;
			}

			transform.localEulerAngles = localRotation;
		}


	}
}