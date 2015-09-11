using SimpleJSON;
using UnityEngine;
using MS.Model;
using System.Collections.Generic;

namespace MS.Model
{
    public class Grid : ModelElement
    {
        protected Tile[,]       m_Tiles;
        protected MapElement[,] m_Elements;
        protected int           hSize;
        protected int           vSize;

        // Properties

        /// <summary>
        /// Number of tiles in the horizontal axis.
        /// </summary>
        /// <value>The size of the horizontal.</value>
        public int HorizontalSize
        {
            get { return hSize; }
        }

        /// <summary>
        /// Number of tiles in the vertical axis.
        /// </summary>
        /// <value>The size of the vertical.</value>
        public int VerticalSize
        {
            get { return vSize;  }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MS.Grid"/> class.
        /// </summary>
        /// <param name="x">Size in the horizontal axis.</param>
        /// <param name="y">Size in the vertical axis.</param>
        public Grid(int x, int y)
        {
            hSize = x;
            vSize = y;

            m_Tiles = new Tile[hSize, vSize];

            for (int i = 0; i < hSize; ++i)
            {
                for (int j = 0; j < vSize; ++j)
                {
                    m_Tiles[i, j] = new Tile();
                    m_Tiles[i, j].X = i;
                    m_Tiles[i, j].Y = j;
                }
            }

            m_Elements = new MapElement[hSize, vSize];
        }

        /// <summary>
        /// Gets the tile in the given coordinate.
        /// </summary>
        /// <returns>Tile stored in the given coordinate, or null if the coordinates are not correct.</returns>
        /// <param name="axial">Axial coordinate of the tile to get.</param>
        public Tile GetTile(Vector2 axial)
        {
            return GetTile((int)axial.x, (int)axial.y);
        }

        /// <summary>
        /// Gets the tile in the given coordinate.
        /// </summary>
        /// <returns>Tile stored in the given coordinate, or null if the coordinates are not correct.</returns>
        /// <param name="x">Horizontal coordinate of the tile.</param>
        /// <param name="y">Vertical coordinate of the tile.</param>
        public Tile GetTile(int x, int y)
        {
            if (x < 0 || x >= hSize || y < 0 || y >= vSize)
            {
                return null;
            }

            return m_Tiles[x, y];
        }

        /// <summary>
        /// Sets the tile for the coordinates given.
        /// </summary>
        /// <param name="axial">Axial coordinate where the tile will be placed.</param>
        /// <param name="tile">Tile to place in the position.</param>
        public void SetTile(Vector2 axial, Tile tile)
        {
            SetTile((int)axial.x, (int)axial.y, tile);
        }

        /// <summary>
        /// Sets the tile for the coordinates given.
        /// </summary>
        /// <param name="x">Horizontal coordinate where the tile will be placed.</param>
        /// <param name="y">Vertical coordinate where the tile will be placed.</param>
        /// <param name="tile">Tile to place in the position.</param>
        public void SetTile(int x, int y, Tile tile)
        {
            if (x < 0 || x > hSize || y < 0 || y > vSize)
            {
                throw new System.IndexOutOfRangeException();
            }

            m_Tiles[x, y] = tile;
        }

        /// <summary>
        /// Gets the map element placed in the given position.
        /// </summary>
        /// <returns>Map element placed in the tile or null if there is no element.</returns>
        /// <param name="axial">Axial coordinate of the tile where the element is placed.</param>
        public MapElement GetElement(Vector2 axial)
        {
            return GetElement((int)axial.x, (int)axial.y);
        }

        /// <summary>
        /// Gets the map element placed in the given position.
        /// </summary>
        /// <returns>Map element placed in the tile or null if there is no element.</returns>
        /// <param name="x">Horizontal coordinate of the tile where the element is placed.</param>
        /// <param name="y">Vertical coordinate of the tile where the element is placed.</param>
        public MapElement GetElement(int x, int y)
        {
            return m_Elements[x, y];
        }

        public MapElement GetElement(Player owner, string name)
        {
            foreach (MapElement element in m_Elements)
            {
                if (element.Name == name)
                {
                    OwnableMapElement ownable;

                    ownable = element as OwnableMapElement;

                    if (ownable != null && ownable.Owner == owner)
                    {
                        return element;
                    }
                }                
            }

            return null;
        }

        /// <summary>
        /// Sets the element in the given tile.
        /// </summary>
        /// <param name="axial">Axial coordinate of the tile where to place the element.</param>
        /// <param name="element">Element to place in the tile.</param>
        public void SetElement(Vector2 axial, MapElement element)
        {
            SetElement((int)axial.x, (int)axial.y, element);
        }

        /// <summary>
        /// Sets the element in the given tile.
        /// </summary>
        /// <param name="x">Horizontal coordinate of the tile where to place the element.</param>
        /// <param name="y">Vertical coordinate of the tile where to place the element.</param>
        /// <param name="element">Element to place in the tile.</param>
        public void SetElement(int x, int y, MapElement element)
        {
            m_Elements[x,y] = element;
        }

        public IEnumerable<OwnableMapElement> GetElements(Player owner)
        {
            List<OwnableMapElement>    elements;
            OwnableMapElement          ownableElement;

            elements = new List<OwnableMapElement>();

            foreach (MapElement element in m_Elements)
            {
                ownableElement = element as OwnableMapElement;

                if (ownableElement != null && ownableElement.Owner == owner)
                {
                    elements.Add(ownableElement);
                }
            }

            return elements;

        }

        /// <summary>
        /// Gets the lowest height among all neighbours of the given tile.
        /// </summary>
        /// <returns>Lowest height among neighbours.</returns>
        /// <param name="x">Horizontal coordinate of the center tile to search from.</param>
        /// <param name="y">Vertical coordinate of the center tile to search from.</param>
        public int GetLowestNeighborHeight(int x, int y)
        {
            var neighbors = Hexagon.GetCubeNeighbors(Hexagon.AxialToCube(x,y));

            Tile currentTile;
            int lowestHeight;

            lowestHeight = int.MaxValue;

            foreach (Vector3 cubePos in neighbors)
            {
                currentTile = GetTile(Hexagon.CubeToAxial(cubePos));

                if (currentTile != null && currentTile.Height < lowestHeight)
                {
                    lowestHeight = currentTile.Height;
                }
            }
            return lowestHeight;
        }

        public override void FromJSON(JSONNode node)
        {
            hSize = node["size"]["horizontal"].AsInt;
            vSize = node["size"]["vertical"].AsInt;

            m_Tiles = new Tile[hSize, vSize];

            int x;
            int y;
            Tile tile;

            x = 0;
            foreach (JSONNode row in node["tiles"].AsArray)
            {
                y = 0;
                foreach (JSONNode tileNode in row.AsArray)
                {
                    tile = new Tile();

                    tile.FromJSON(tileNode);

                    m_Tiles[x, y] = tile;

                    y++;
                }
                x++;
            }

            m_Elements = new MapElement[hSize, vSize];

            MapElement element;

            foreach (JSONNode elementNode in node["elements"].AsArray)
            {
                element = MapElement.Create(elementNode["x"].AsInt, elementNode["y"].AsInt, elementNode["name"]);
                element.FromJSON(elementNode);

                m_Elements[element.X, element.Y] = element;
            }
        }

        public override JSONNode ToJSON()
        {
            JSONClass json = new JSONClass();
            JSONArray tilesArray;
            JSONArray elementsArray;
            JSONArray row;
            JSONNode tileNode;
            JSONNode elementNode;

            JSONClass size = new JSONClass();

            size.Add("horizontal", new JSONData(hSize));
            size.Add("vertical", new JSONData(vSize));

            json.Add("size", size);

            tilesArray = new JSONArray();

            for (int x = 0; x < hSize; x++)
            {
                row = new JSONArray();
                for (int y = 0; y < vSize; y++)
                {
                    if (m_Tiles[x, y] != null)
                    {
                        tileNode = m_Tiles[x, y].ToJSON();

                        row.Add(tileNode);
                    }
                }
                tilesArray.Add(row);
            }

            json.Add("tiles", tilesArray);

            elementsArray = new JSONArray();

            foreach (MapElement element in m_Elements)
            {
                if (element != null)
                {
                    elementNode = element.ToJSON();

                    elementsArray.Add(elementNode);
                }
            }

            json.Add("elements", elementsArray);

            return json;
        }
    }
}
