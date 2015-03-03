using UnityEngine;
using System;
using MS.Core;

namespace MS.View
{
    public class MapView : View<MS.Model.Map>
    {
        #region Attributes

        // Tiles
        public GameObject           TilesContainer;
        public GrassTileView        GrassTilePrefab;
        public WaterTileView        WaterTilePrefab;
        public MountainTileView     MountainTilePrefab;
        public ForestTileView       ForestTilePrefab;
        // ---

        // Elements
        public GameObject           ElementsContainer;
        public CityView             HumanCityPrefab;
        public PickableResourceView PickableResourcePrefab;
        public ResourceProducerView ResourceProducerPrefab;
        public MapElementView       MissingElementPrefab;
        // ---

        public GameObject           SelectorPrefab;

        public int                  TileWidth;
        public int                  TileHeight;

        private TileView[,]         m_tiles;
        private MapElementView[,]   m_elements;

        /// <summary>
        /// Plane used to detect interaction between the cursor and the map.
        /// </summary>
        private Plane               m_plane;

        /// <summary>
        /// Visual selector for tiles
        /// </summary>
        private GameObject          m_selector;

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the view to show the state of the model.
        /// </summary>
        public override void UpdateView()
        {
            TilesContainer.transform.RemoveChildren();
            ElementsContainer.transform.RemoveChildren();

            m_tiles     =   new TileView[m_model.Grid.HorizontalSize, m_model.Grid.VerticalSize];
            m_elements  =   new MapElementView[m_model.Grid.HorizontalSize, m_model.Grid.VerticalSize];

            m_plane     =   new Plane (Vector3.back, this.transform.position);
            m_selector  =   Instantiate(SelectorPrefab, LocalToWorld(0, 0), Quaternion.identity) as GameObject;

            m_selector.transform.parent = this.transform;

            Resources.UnloadUnusedAssets();
            System.GC.Collect();

            for (int y = 0; y < m_model.Grid.VerticalSize; ++y)
            {
                for (int x = 0; x < m_model.Grid.HorizontalSize; ++x)
                {
                    TileView tile;

                    tile = CreateTile(m_model.Grid[x,y], x, y);

                    tile.Location = new Vector2(x, y);
                    tile.UpdateView();

                    m_tiles[x, y] = tile;
                }
            }

            foreach (Model.MapElement element in this.m_model.Elements)
            {
                MapElementView view;

                view = CreateElement(element);
                view.UpdateView();

                m_elements[(int)element.Location.x, (int)element.Location.y] = view;
            }
            
            // Add it to the static batching system
            StaticBatchingUtility.Combine(TilesContainer);
            StaticBatchingUtility.Combine(ElementsContainer);
        }

        /// <summary>
        /// Selects a given tile, positioning the selection marker over it.
        /// </summary>
        /// <param name="x">The x map local coordinate.</param>
        /// <param name="y">The y map local coordinate.</param>
        public void SelectTile(float x, float y)
        {
            Vector3 finalPosition;

            finalPosition = LocalToWorld(x, y);
            m_selector.transform.position = finalPosition;
        }

        /// <summary>
        /// Selects a tile given world coordinates.
        /// </summary>
        /// <param name="x">The x world coordinate.</param>
        /// <param name="y">The y world coordinate.</param>
        /// <param name="z">The z world coordinate.</param>
        public void SelectTile(float x, float y, float z)
        {
            SelectTile(WorldToLocal(x, y, z));
        }

        /// <summary>
        /// Selects the tile in a given position.
        /// </summary>
        /// <param name="worldPosition">World position.</param>
        public void SelectTile(Vector3 worldPosition)
        {
            SelectTile(worldPosition.x, worldPosition.y, worldPosition.z);
        }

        /// <summary>
        /// Selects the tile at a map local position.
        /// </summary>
        /// <param name="localPosition">Local position.</param>
        public void SelectTile(Vector2 localPosition)
        {
            SelectTile((int)localPosition.x, (int)localPosition.y);
        }

        /// <summary>
        /// Calculates the nearest grid tile to the coordinates passed.
        /// </summary>
        /// <returns>The to cube.</returns>
        /// <param name="cubePos">Cube position.</param>
        public static Vector3 RoundToCube (Vector3 cubePos)
        {
            Vector3 pos;

            int rx = Mathf.RoundToInt (cubePos.x);
            int ry = Mathf.RoundToInt (cubePos.y);
            int rz = Mathf.RoundToInt (cubePos.z);

            float x_diff = Mathf.Abs (rx - cubePos.x);
            float y_diff = Mathf.Abs (ry - cubePos.y);
            float z_diff = Mathf.Abs (rz - cubePos.z);

            if (x_diff > y_diff && x_diff > z_diff)
            {
                rx = -ry - rz;
            }
            else if (y_diff > z_diff)
            {
                ry = -rx - rz;
            }
            else
            {
                rz = -rx - ry;
            }

            pos = new Vector3 (rx, ry, rz);

            return pos;
        }

        /// <summary>
        /// Converts from axial coordinates to cube coordinates.
        /// </summary>
        /// <returns>The to cube.</returns>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public static Vector3 AxialToCube(float x, float y)
        {
            float cubeX = x;
            float cubeZ = y;
            float cubeY = - cubeX - cubeZ;

            return new Vector3 (cubeX, cubeY, cubeZ);
        }

        /// <summary>
        /// Converts from axial coordinates to cube coordinates.
        /// </summary>
        /// <returns>The to cube.</returns>
        /// <param name="axialPos">Axial position.</param>
        public static Vector3 AxialToCube (Vector2 axialPos)
        {
            return AxialToCube((int)axialPos.x, (int)axialPos.y);
        }

        /// <summary>
        /// Converts from axual coordinate to cube coordinates.
        /// </summary>
        /// <returns>The to axial.</returns>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="z">The z coordinate.</param>
        public static Vector2 CubeToAxial(int x, int y, int z)
        {
            return new Vector2(x, z);
        }

        /// <summary>
        /// Converst from cube coordinates to axial coordinates.
        /// </summary>
        /// <returns>The to axial.</returns>
        /// <param name="cubePos">Cube position.</param>
        public static Vector2 CubeToAxial (Vector3 cubePos)
        {
            return CubeToAxial((int)cubePos.x, (int)cubePos.y, (int)cubePos.z);
        }

        #endregion

        #region Protected and private methods

        /// <summary>
        /// Creates a new tile to display information about a given tile of the map model.
        /// </summary>
        /// <returns>The tile.</returns>
        /// <param name="model">Map's model tile to associate with the new view.</param>
        /// <param name="x">The x coordinate in the grid.</param>
        /// <param name="y">The y coordinate in the grid.</param>
        protected TileView CreateTile(MS.Model.Tile model, int x, int y)
        {
            GameObject  obj;
            TileView    prefab;
            TileView    tile;
            Vector3     worldPosition;

            prefab = SelectPrefab(model.Type);

            worldPosition   =   LocalToWorld(x, y);
            obj             =   Instantiate(prefab.gameObject, worldPosition, Quaternion.identity) as GameObject;
            tile            =   obj.GetComponent<TileView>();

            tile.BindTo(model);
            tile.transform.parent = TilesContainer.transform;
            tile.gameObject.name = string.Format("Tile ({0},{1}) @ {2}", x, y, model.Type);

            return tile;
        }

        /// <summary>
        /// Creates a map element that can be placed in the map.
        /// </summary>
        /// <returns>The element.</returns>
        /// <param name="model">Model.</param>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        protected MapElementView CreateElement(MS.Model.MapElement model)
        {
            GameObject      obj;
            MapElementView  prefab;
            MapElementView  element;
            Vector3         worldPosition;
            float x;
            float y;

            try
            {
                prefab = SelectPrefab(model);
            }
            catch (Exceptions.WrongType ex)
            {
                prefab = MissingElementPrefab;
                MS.Debug.Core.LogError(ex.Message);
            }

            x               =   model.Location.x;
            y               =   model.Location.y;
            worldPosition   =   LocalToWorld(x, y);
            obj             =   Instantiate(prefab.gameObject, worldPosition, Quaternion.identity) as GameObject;
            element         =   obj.GetComponent<MapElementView>();

            element.BindTo(model);
            element.transform.parent = ElementsContainer.transform;
            element.gameObject.name = element.ToString();

            return element;
        }

        /// <summary>
        /// Selects what prefab use to represent a given type of terrain.
        /// </summary>
        /// <returns>The prefab that should be used to represent the terrain type.</returns>
        /// <param name="terrain">Terrain type we want to know the prefab to use.</param>
        protected TileView SelectPrefab(MS.Model.Tile.TerrainType terrain)
        {
            switch (terrain)
            {
                case MS.Model.Tile.TerrainType.Grass:       return GrassTilePrefab;
                case MS.Model.Tile.TerrainType.Water:       return WaterTilePrefab;
                case MS.Model.Tile.TerrainType.Mountain:    return MountainTilePrefab;
                case MS.Model.Tile.TerrainType.Forest:      return ForestTilePrefab;
                default:                                    return null;
            }
        }

        /// <summary>
        /// Selects the prefab to represent the map element specified.
        /// </summary>
        /// <returns>The prefab.</returns>
        /// <param name="model">Model.</param>
        protected MapElementView SelectPrefab(MS.Model.MapElement model)
        {
            if (model is Model.City)
            {
                return HumanCityPrefab;
            }
            else if (model is Model.PickableResource)
            {
                return PickableResourcePrefab;
            }
            else if (model is Model.ResourceProducer)
            {
                return ResourceProducerPrefab;
            }
            else
            {
                throw new Exceptions.MissingPrefabForType(typeof(MS.Model.MapElement));
            }
        }

        /// <summary>
        /// Converts local coordinates to world coordiantes (relative to the parent).
        /// </summary>
        /// <returns>The to world.</returns>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        protected Vector3 LocalToWorld(float x, float y)
        {
            Vector2 pos = new Vector2 (0.0f, 0.0f);

            x = x - m_model.Grid.HorizontalSize / 2;
            y = y - m_model.Grid.VerticalSize / 2;

            pos.x = this.transform.position.x + (TileWidth / 100.0f) * Mathf.Sqrt (3) * (x + y / 2.0f);
            pos.y = this.transform.position.y + (TileHeight / 100.0f) * 3.0f / 2.0f * y;

            return pos;
        }

        /// <summary>
        /// Converts local coordinates to world coordinates (relative to the parent).
        /// </summary>
        /// <returns>The to world.</returns>
        /// <param name="localPosition">Local position.</param>
        protected Vector3 LocalToWorld(Vector2 localPosition)
        {
            return LocalToWorld(localPosition.x, localPosition.y);
        }

        /// <summary>
        /// Converts a position in world coordinates to a local coordinate in the grid.
        /// </summary>
        /// <returns>The to local.</returns>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="z">The z coordinate.</param>
        protected Vector2 WorldToLocal(float x, float y, float z)
        {
            Vector2 pos;

            float approximateX;
            float approximateY;

            approximateX = (x * Mathf.Sqrt(3) / 3.0f - y / 3.0f) / (TileWidth / 100.0f) - this.transform.position.x;
            approximateY = (y * 2.0f / 3.0f ) / (TileHeight / 100.0f) - this.transform.position.y;

            approximateX += m_model.Grid.HorizontalSize / 2;
            approximateY += m_model.Grid.VerticalSize / 2;

            // And now we find the nearest hexagon to that position
            Vector3 cubePos = AxialToCube (approximateX, approximateY);
            cubePos = RoundToCube (cubePos);

            pos = CubeToAxial (cubePos);

            return pos;
        }

        /// <summary>
        /// Converts a position in world coordinates to a local coordinate in the grid.
        /// </summary>
        /// <returns>The to local.</returns>
        /// <param name="worldPosition">World position.</param>
        protected Vector2 WorldToLocal(Vector3 worldPosition)
        {
            return WorldToLocal(worldPosition.x, worldPosition.y, worldPosition.z);
        }

        private void HandleInput()
        {
            Vector3     mousePos;
            Ray         ray;
            float       distance;

#if UNITY_STANDALONE || UNITY_EDITOR
            ray = Camera.main.ScreenPointToRay (MS.Core.InputManager.CursorPosition);

            if (m_plane.Raycast (ray, out distance))
            {
                mousePos = ray.GetPoint (distance);
                SelectTile(mousePos);
            }
#elif UNITY_ANDROID || UNITY_IOS
            if (MS.Core.InputManager.GetButton("Touch"))
            {
                ray = Camera.main.ScreenPointToRay (MS.Core.InputManager.CursorPosition);

                if (m_plane.Raycast (ray, out distance))
                {
                    mousePos = ray.GetPoint (distance);
                    SelectTile(mousePos);
                }
            }
#endif
        }

        #endregion

        #region Unity methods

        void LateUpdate()
        {
            HandleInput();
        }

        #endregion
    }
}
