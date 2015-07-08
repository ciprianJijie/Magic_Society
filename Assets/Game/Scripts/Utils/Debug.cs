using UnityEngine;
using System.Collections;

namespace MS
{
	namespace Debug
	{
		public static class Core
		{
			[System.Diagnostics.Conditional("DEBUG_CORE")]
			public static void Log(object message)
			{
				UnityEngine.Debug.Log (message);
			}

			[System.Diagnostics.Conditional("DEBUG_CORE")]
			public static void LogError(object message)
			{
				UnityEngine.Debug.LogError(message);
			}
		}
	}
}

