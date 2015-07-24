using SimpleJSON;
using UnityEngine;

namespace MS
{
    public class Grid : ModelElement
    {
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
        }

        public Tile GetTile(Vector2 axial)
        {
            return GetTile((int)axial.x, (int)axial.y);
        }

        public Tile GetTile(int x, int y)
        {
            if (x < 0 || x >= hSize || y < 0 || y >= vSize)
            {
                return null;
            }

            return m_Tiles[x, y];
        }

        public void SetTile(int x, int y, Tile tile)
        {
            if (x < 0 || x > hSize || y < 0 || y > vSize)
            {
                throw new System.IndexOutOfRangeException();
            }

            m_Tiles[x, y] = tile;
        }
        
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
        }

        public override JSONNode ToJSON()
        {
            JSONNode json = JSON.Parse(Resources.Load<TextAsset>("Data/JSON/Templates/Grid").text);
            JSONArray tilesArray;
            JSONArray row;
            JSONNode tileNode;

            json["size"]["horizontal"].AsInt = hSize;
            json["size"]["vertical"].AsInt = vSize;

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

            json["tiles"] = tilesArray;

            return json;
        }

        // Properties

        public int HorizontalSize
        {
            get { return hSize; }
        }

        public int VerticalSize
        {
            get { return vSize;  }
        }

        // Variables

        public string ID;

        protected Tile[,] m_Tiles;
        protected int hSize;
        protected int vSize;
    }
}
