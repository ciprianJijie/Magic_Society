using SimpleJSON;
using UnityEngine;

namespace MS
{
    public class Map : ModelElement
    {
        public Map()
        {

        }

        public Map(string name, int x, int y)
        {
            Name = name;

            Tiles = new Grid(x, y);
        }

        public void Resize(int hSize, int vSize)
        {
            Grid newGrid;
            int maxX;
            int maxY;

            newGrid = new Grid(hSize, vSize);
            maxX = Mathf.Min(hSize, Tiles.HorizontalSize);
            maxY = Mathf.Min(vSize, Tiles.VerticalSize);

            for(int x = 0; x < maxX; ++x)
            {
                for (int y = 0; y < maxY; ++y)
                {
                    newGrid.SetTile(x, y, Tiles.GetTile(x, y));
                }
            }

            Tiles = newGrid;

            System.GC.Collect();
        }

        public override void FromJSON(JSONNode json)
        {
            Name = json["name"];

            Tiles = new Grid(0, 0);

            Tiles.FromJSON(json["grid"]);
        }

        public override JSONNode ToJSON()
        {
            JSONNode json;

            json = JSON.Parse((Resources.Load<TextAsset>("Data/JSON/Templates/Map").text));

            json["name"] = Name;
            json["grid"] = Tiles.ToJSON();

            return json;
        }

        public string   Name;
        public Grid     Tiles;
    }
}
