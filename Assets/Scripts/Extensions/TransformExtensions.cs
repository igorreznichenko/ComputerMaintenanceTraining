using ComputerMaintenanceTraining.AssemblyObjects;
using ComputerMaintenanceTraining.Enums;
using UnityEngine;

namespace ComputerMaintenanceTraining.Extensions
{
	public static class TransformExtensions
	{
		public static void SetLocalRotationForAxis(this Transform transform, float rotation, Axis axis)
		{
			Vector3 localRotation = transform.localEulerAngles;

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

		public static void SetLocalPositionForAxis(this Transform transform, float position, Axis axis)
		{
			Vector3 localPosition = transform.localPosition;

			switch (axis)
			{
				case Axis.None:
					return;

				case Axis.X:
					localPosition.x = position;
					break;

				case Axis.Y:
					localPosition.y = position;
					break;

				case Axis.Z:
					localPosition.z = position;
					break;
			}

			transform.localPosition = localPosition;
		}

		public static Quaternion TransformWorldToLocalRotation(this Transform transform, Quaternion worldRotation)
		{
			return Quaternion.Inverse(transform.rotation) * worldRotation;
		}
	}
}