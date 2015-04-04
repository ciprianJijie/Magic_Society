using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;

namespace MS.Core
{
	public static class Localization
	{
		public enum Language { English, Spanish }

		public static Language CurrentLanguage;
        private static Dictionary<string, string>   m_localization;

        public static void LoadLanguage(Language language)
        {
            if (m_localization != null)
            {
                m_localization.Clear();
            }
            else
            {
                m_localization = new Dictionary<string, string>();
            }

            string      filePath;
            string      text;
            JSONNode    root;
            JSONArray   jsonArray;

            filePath    =   GetLanguageFilePath(language);
            UnityEngine.Debug.Log("Parsing localization file at " + filePath);
            text        =   ((TextAsset)Resources.Load(filePath, typeof(TextAsset))).text;
            root        =   JSON.Parse(text);
            jsonArray   =   root["values"].AsArray;

            foreach (JSONNode node in jsonArray)
            {
                string localizationID;
                string localizationText;

                localizationID      =   node["ID"];
                localizationText    =   node["Text"];

                m_localization.Add(localizationID, localizationText);
            }
        }

        public static string GetString(string ID)
        {
            string text;

            try
            {
                text = m_localization[ID];
            }
            catch(Exception exp)
            {
                UnityEngine.Debug.LogError(exp.Message);
                text = "$MISSING_LOCALIZATION$";
            }

            return text;
        }

        private static string GetLanguageFilePath(Language language)
        {
            return string.Format("Localization/{0}", language.ToString());
            //return string.Format("{0}/Localization/{1}.json", Application.persistentDataPath, language.ToString());
        }
	}
}

