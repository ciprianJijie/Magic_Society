using UnityEngine;

namespace MS
{
	public class GridController : Controller<TileView, Tile>
	{
		// Attributes
		public float HexagonSize = 1.0f;

		[HideInInspector]
        public float TileWidth;

		[HideInInspector]
        public float TileHeight;

        // Private variables
        private Grid m_ShownGrid;

		// Properties
		public Grid Grid
		{
			get
			{
                return m_ShownGrid;
            }
		}

		// Public methods

		/// <summary>
		/// Creates the views necessary to show the provided grid.
		/// </summary>
		/// <param name="grid">Grid to show using tile views.</param>
		public void Show(Grid grid)
		{
			ClearViews();

			m_ShownGrid = grid;

			for (int x = 0; x < grid.HorizontalSize; x++)
			{
				for (int y = 0; y < grid.VerticalSize; y++)
				{
					var tile = CreateView(grid.GetTile(x, y));

					tile.name = string.Format("Tile[{0},{1}]", x, y);
					tile.transform.position = LocalToWorld(x, y);
					tile.Owner = this;
				}
			}

            TileWidth 		= 	Hexagon.Width(HexagonSize);
            TileHeight 		= 	Hexagon.Height(HexagonSize);

			// Update views
			foreach (TileView tile in m_Views)
			{
				tile.UpdateView();
            }
        }

		/// <summary>
		/// Updates the view for the tile in the given position.
		/// </summary>
		/// <param name="axial">Local coordinates of the tile to update (axial).</param>
		/// <return>true if the tile was updated, false otherwise.</return>
		public bool UpdateView(Vector2 axial, bool expandToNeighbors)
		{
			return UpdateView((int)axial.x, (int)axial.y, expandToNeighbors);
		}

		/// <summary>
		/// Updates the view for the tile in the given position.
		/// </summary>
		/// <param name="x">Horizontal local position (axial) of the tile to update.</param>
		/// <param name="y">Vertical local position (axial) of the tile to update.</param>
		/// <return>true if the tile was updated, false otherwise.</return>
		public bool UpdateView(int x, int y, bool expandToNeighbors)
		{
			Tile 		tileToUpdateView;
			TileView 	viewToUpdate;

			tileToUpdateView 	= 	m_ShownGrid.GetTile(x, y);
			viewToUpdate		=	FindView(tileToUpdateView);

			if (tileToUpdateView == null || viewToUpdate == null)
			{
				return false;
			}

			viewToUpdate.UpdateView();

			if (expandToNeighbors)
			{
                var neighbors = Hexagon.GetAxialNeighbors(x, y);

				foreach (Vector2 tilePos in neighbors)
				{
                    UpdateView(tilePos, false);
                }
            }

			return true;
		}

		/// <summary>
		/// Converts from a grid-local axial coordinate to Unity's world coordinates.
		/// </summary>
		/// <param name="axial">Grid-local axial coordinate to convert.</param>
		/// <return>World position corresponding to the center point of the local position.</return>
		public Vector3 LocalToWorld(Vector2 axial)
		{
			return LocalToWorld(axial.x, axial.y);
		}

		/// <summary>
		/// Converts from a grid-local axial coordinate to Unity's world coordinates.
		/// </summary>
		/// <param name="x">Horizontal local position to convert.</param>
		/// <param name="y">Vertical local position to convert.</param>
		/// <return>World position corresponding to the center point of the local position.</return>
		public Vector3 LocalToWorld(float x, float y)
		{
			Vector3 pos = new Vector3(0.0f, 0.0f, 0.0f);

            x = x - (float) m_ShownGrid.HorizontalSize / 2f;
            y = y - (float) m_ShownGrid.VerticalSize / 2f;

            pos.x = this.transform.position.x + (HexagonSize * Mathf.Sqrt (3f) * (x + y / 2.0f));
            pos.y = this.transform.position.y;
            pos.z = this.transform.position.z + (HexagonSize * 3.0f / 2.0f * y);

            return pos;
		}

		/// <summary>
		/// Calculates the grid local coordinate corresponding to the world position provided.
		/// </summary>
		/// <param name="world">World position to convert.</param>
		/// <return>Local position in grid axial coordinates.</return>
		public Vector2 WorldToLocal(Vector3 world)
		{
			return WorldToLocal(world.x, world.y, world.z);
		}

		/// <summary>
		/// Calculates the grid local coordinate corresponding to the world position provided.
		/// </summary>
		/// <param name="x">Lateral world coordinate.</param>
		/// <param name="y">Vertical world coordinate.</param>
		/// <param name="z">Forward world coordinate.</param>
		/// <return>Local position in grid axial coordinates.</return>
		public Vector2 WorldToLocal(float x, float y, float z)
		{
			Vector2 pos;

            float approximateX;
            float approximateY;

            approximateX = (x * (Mathf.Sqrt(3.0f) / 3.0f) - (z / 3.0f) / HexagonSize);
            approximateY = (z * (2.0f / 3.0f) / HexagonSize);

            approximateX += (float)m_ShownGrid.HorizontalSize / 2f;
            approximateY += (float)m_ShownGrid.VerticalSize / 2f;

            // And now we find the nearest hexagon to that position
            Vector3 cubePos = Hexagon.AxialToCube(approximateX, approximateY);
            cubePos = Hexagon.RoundToCube(cubePos);

            pos = Hexagon.CubeToAxial(cubePos);

            return pos;
		}

		/// <summary>
		/// World position for the grid tile with height applied.
		/// </summary>
		/// <param name="x">Horizontal local grid coordinate (axial).</param>
		/// <param name="y">Vertical local grid coordinate (axial).</param>
		/// <return>World position of the tile with height applied.</return>
		public Vector3 GetSelectorPosition(int x, int y)
        {
            Vector3     worldPosition;
            float       height;

            worldPosition = LocalToWorld(x, y);

            if (x < 0 || y < 0 || x >= m_ShownGrid.HorizontalSize || y >= m_ShownGrid.VerticalSize)
            {
                height = 0;
            }
            else
            {
                height = m_ShownGrid.GetTile(x, y).Height;
            }

            return worldPosition + Vector3.up * height * 0.5f;
        }
	}
}
