using UnityEngine;
using System.Text;

namespace MS.Utils
{
	public static class Path
	{
		/// <summary>
		///	Builds a string with the absolute path to scenario with the specified name.
		/// </summary>
		/// <param name="scenarioName">Name of the scenario, without .json. If the scenario is in a subfolder, include the folder name like '/subfolder/scenario'.</param>
		public static string ToScenario(string scenarioName)
		{
			return "Data/Scenarios/" + scenarioName;
		}

		/// <summary>
		/// Memory safe version to obtain the path to a scene. Requires a previous StringBuilder object.
		/// It takes care to have enough capacity for the resulting string, allocating more space if necesary.
		/// </summary>
		/// <param name="scenarioName">Name of the scenario to build the path to.</param>
		/// <param name="buffer">Previous instance of a stringbuilder. Required to avoid memory leaks during string operations.</param>
		public static void ToScenario(string scenarioName, ref StringBuilder buffer)
		{
			int minLength;

			minLength = scenarioName.Length + 24;

			buffer.Remove(0, buffer.Length);
			buffer.EnsureCapacity(minLength);
			buffer.AppendFormat("Data/Scenarios/{0}", scenarioName);
		}
	}
}