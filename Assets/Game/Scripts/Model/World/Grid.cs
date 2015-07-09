using SimpleJSON;

namespace MS
{
    public class Grid : ModelElement
    {
        public Grid(int x, int y)
        {
            m_Tiles = new Tile[x, y];
        }

        public override void FromJSON(JSONNode node)
        {
            int hSize;
            int vSize;

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
            JSONNode json = new JSONNode();
            JSONArray tilesArray;

            json["size"]["horizontal"]  =   m_Tiles.Length.ToString();
            json["size"]["vertical"]    =   m_Tiles.GetLength(0).ToString();

            tilesArray = new JSONArray();

            for (int x = 0; x < m_Tiles.Length; x++)
            {
                for (int y = 0; y < m_Tiles.GetLength(0); y++)
                {
                    tilesArray.Add(m_Tiles[x,y].ToJSON());
                }
            }

            json["tiles"] = tilesArray;

            return json;
        }

        public string ID;

        protected Tile[,] m_Tiles;
    }
}
