using System.Collections;
using TMPro;
using UnityEngine;

namespace ComputerMaintenanceTraining.Extensions
{
	public static class TMPExtensions
	{
		public static IEnumerator PrintTextBySymbolsCoroutine(this TMP_Text outText, string text, float printSymbolTime)
		{
			outText.text = string.Empty;

			int index = 0;

			while (index < text.Length)
			{
				outText.text += text[index];
				index++;

				yield return new WaitForSeconds(printSymbolTime);
			}
		}
	}
}