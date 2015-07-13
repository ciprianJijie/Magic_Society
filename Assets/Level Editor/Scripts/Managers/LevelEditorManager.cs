using UnityEngine;
using SimpleJSON;
using System.IO;

namespace MS
{
	public class LevelEditorManager : Singleton<LevelEditorManager>
	{
		public void ShowFileBrowserWindow()
		{
            m_FileBrowserWindow = Instantiate(FileBrowserPrefab.gameObject);
            m_FileBrowserWindow.transform.SetParent(WindowsContainer, false);
            //m_FileBrowserWindow.transform.position = Vector3.zero;
        }

		public void HideFileBrowserWindow()
		{
            Destroy(m_FileBrowserWindow);
        }

		public void ShowNameLevelWindow()
		{
            Instantiate(NameLevelPrefab).transform.SetParent(WindowsContainer, false);
		}

		public void New(string levelName, int x, int y)
		{
            m_CurrentMap = new Map(levelName, x, y);

            m_CurrentFilePath = Application.streamingAssetsPath + "/Maps/" + levelName + ".json";

            Debug.Core.Log("Created map " + m_CurrentMap.Name + " that will be saved at " + m_CurrentFilePath);
        }

		public void Load(string file)
		{
			StreamReader 	reader;
            string 			text;
            JSONNode 		json;

            reader 				= 	new StreamReader(file);
            text 				= 	reader.ReadToEnd();
            json 				= 	JSONNode.Parse(text);
            m_CurrentMap 		= 	new Map();
			m_CurrentFilePath 	= 	file;

            m_CurrentMap.FromJSON(json);
        }

		public void Save()
		{
            JSONNode json;

			json = m_CurrentMap.ToJSON();

            Debug.Core.Log("Saving map " + json["name"] + " in file " + m_CurrentFilePath);

            Debug.Core.Log("JSON: " + json.ToString());

            //json.SaveToFile(m_CurrentFilePath);
			System.IO.File.WriteAllText(m_CurrentFilePath, json.ToString(""));
        }

        public Transform 		WindowsContainer;
        public GameObject 		FileBrowserPrefab;
        public GameObject 		NameLevelPrefab;

        protected Map 			m_CurrentMap;
        protected GameObject 	m_FileBrowserWindow;
        protected string 		m_CurrentFilePath;

    }
}
