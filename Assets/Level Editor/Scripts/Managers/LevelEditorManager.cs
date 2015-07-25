using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using System.IO;

namespace MS
{
	public class LevelEditorManager : Singleton<LevelEditorManager>
	{
		// Attributes
		public Transform 		WindowsContainer;
        public Transform        WorldContainer;
        public GridController   GridController;
        public GameObject 		FileBrowserPrefab;
        public GameObject 		NameLevelPrefab;
        public GameObject       ResizeWindowPrefab;
        public MouseToGrid      MouseToGridHandler;

        [HideInInspector]
        public Brush            Brush;

        protected Map 			m_CurrentMap;
        protected GameObject 	m_FileBrowserWindow;
        protected string 		m_CurrentFilePath;

		// Public methods

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

        public void ShowResizeWindow()
        {
            Instantiate(ResizeWindowPrefab).transform.SetParent(WindowsContainer, false);
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

        public void Resize(int hSize, int vSize)
        {
            m_CurrentMap.Resize(hSize, vSize);

            GridController.Show(m_CurrentMap.Tiles);
        }

        public void ShowGrid(Grid grid)
        {
            HideGrid();

            GridController.Show(grid);

            GridController.enabled = true;

            Brush = new Brush();

            MouseToGridHandler.enabled = true;
        }

        public void HideGrid()
        {
            GridController.enabled = false;
        }

        // Brush
        public void ChangeBrush(Tile.Terrain terrain)
        {
            Debug.Core.Log("Brush changed to " + terrain);
            Brush.Terrain = terrain;
        }

        public void ChangeBrushTerrain(string terrainString)
        {
            Tile.Terrain terrain;

            terrain = EnumUtils.ParseEnum<Tile.Terrain>(terrainString);

            ChangeBrush(terrain);
        }

		public void ChangeBrush(int height)
		{
            Brush.Height = height;
        }

        public void ApplyBrush(int x, int y)
        {
            if (x < 0 || y < 0 || x >= m_CurrentMap.Tiles.HorizontalSize || y >= m_CurrentMap.Tiles.VerticalSize)
            {
                return;
            }

			if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
			{
				return;
			}

            Tile tile;

            tile = m_CurrentMap.Tiles.GetTile(x, y);

            Brush.Draw(tile);

            // Search Tile View
            GridController.UpdateView(x, y);
        }

        // Unity methods

        protected void Start()
        {
            // Subscribe to events
            MouseToGridHandler.OnMouseLeftClick += ApplyBrush;
            MouseToGridHandler.enabled = false;
        }

        protected void OnDestroy()
        {
            // Unsubscribe to events
            MouseToGridHandler.OnMouseLeftClick -= ApplyBrush;
        }
    }
}
