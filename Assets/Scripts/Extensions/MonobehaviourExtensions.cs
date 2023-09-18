using UnityEngine;

namespace ComputerMaintenanceTraining.Extensions
{
	public static class MonobehaviourExtensions
	{
		public static void KillCoroutine(this MonoBehaviour monobehaviour, ref Coroutine coroutine)
		{
			if (coroutine != null)
			{
				monobehaviour.StopCoroutine(coroutine);

				coroutine = null;
			}
		}
	}
}