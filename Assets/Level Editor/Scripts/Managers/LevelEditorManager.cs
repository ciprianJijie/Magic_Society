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

            ShowGrid(m_CurrentMap.Tiles);
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

            ShowGrid(m_CurrentMap.Tiles);
        }

		public void Save()
		{
            JSONNode json;

			json = m_CurrentMap.ToJSON();

			System.IO.File.WriteAllText(m_CurrentFilePath, json.ToString(""));
        }

        public void ShowGrid(Grid grid)
        {
            HideGrid();

            GridVisualizer.BindTo(grid);
            GridVisualizer.UpdateView();

            GridVisualizer.enabled = true;
        }

        public void HideGrid()
        {
            GridVisualizer.enabled = false;
        }

        public Transform 		WindowsContainer;
        public Transform        WorldContainer;
        public GridView         GridVisualizer;
        public GameObject 		FileBrowserPrefab;
        public GameObject 		NameLevelPrefab;

        protected Map 			m_CurrentMap;
        protected GameObject 	m_FileBrowserWindow;
        protected string 		m_CurrentFilePath;

    }
}
