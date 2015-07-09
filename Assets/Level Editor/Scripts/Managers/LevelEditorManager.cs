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

		public void New(string levelName, int x, int y)
		{
            m_CurrentMap = new Map(levelName, x, y);

            m_CurrentFilePath = Application.streamingAssetsPath + "/Maps/" + levelName + ".json";
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
			json.SaveToFile(m_CurrentFilePath);
        }

        public Transform 		WindowsContainer;
        public GameObject 		FileBrowserPrefab;

        protected Map 			m_CurrentMap;
        protected GameObject 	m_FileBrowserWindow;
        protected string 		m_CurrentFilePath;

    }
}
