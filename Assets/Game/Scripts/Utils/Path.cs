using UnityEngine;
using System.Text;

namespace MS
{
	public static class Path
	{
		/// <summary>
		///	Builds a string with the absolute path to scenario with the specified name.
		/// </summary>
		/// <param name="scenarioName">Name of the scenario, without .json. If the scenario is in a subfolder, include the folder name like '/subfolder/scenario'.</param>
		public static string ToScenario(string scenarioName)
		{
			return Application.streamingAssetsPath + "/Maps/" + scenarioName + ".json";
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

        public static string ToSaveGame(string fileName)
        {
            return Application.streamingAssetsPath + "/Save Games/" + fileName + ".json";
        }

        public static SimpleJSON.JSONNode FileToJSON(string filePath)
        {
            System.IO.StreamReader reader;
            string text;

            reader  =   new System.IO.StreamReader(filePath);
            text    =   reader.ReadToEnd();

            reader.Close();

            return SimpleJSON.JSON.Parse(text);
        }

        public static void JSONToFile(SimpleJSON.JSONNode json, string filePath)
        {
            System.IO.StreamWriter writer;
            string text;

            writer = new System.IO.StreamWriter(filePath);
            text = json.ToString("");

            writer.Write(text);

            writer.Close();
        }

        /// <summary>
        /// Converts a path so it works in the current platform.
        /// </summary>
        /// <param name="path">Path to convert.</param>
        /// <returns>New path suitable for the current platform.</returns>
        public static string PlatformPath(string path)
        {
            string platformPath;

#if UNITY_STANDALONE_WIN
            platformPath = path.Replace(@"/", @"\");
#else
            platformPath = path;
#endif
            return platformPath;
        }
	}
}
