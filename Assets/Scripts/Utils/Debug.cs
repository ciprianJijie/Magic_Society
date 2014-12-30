using UnityEngine;
using System.Collections;

namespace MS
{
	namespace Debug
	{
		public static class Core
		{
			[System.Diagnostics.Conditional("DEBUG_CORE")]
			public static void Log(string message)
			{
				UnityEngine.Debug.Log (message);
			}

			[System.Diagnostics.Conditional("DEBUG_CORE")]
			public static void LogError(string message)
			{
				UnityEngine.Debug.LogError(message);
			}
		}
	}
}

