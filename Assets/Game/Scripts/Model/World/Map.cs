using SimpleJSON;
using UnityEngine;

namespace MS.Model
{
    public class Map : ModelElement
    {
        public Map()
        {

        }

        public Map(string name, int x, int y)
        {
            Name = name;

            Grid = new Grid(x, y);
        }

        public void Resize(int hSize, int vSize)
        {
            Grid newGrid;
            int maxX;
            int maxY;

            newGrid = new Grid(hSize, vSize);
            maxX = Mathf.Min(hSize, Grid.HorizontalSize);
            maxY = Mathf.Min(vSize, Grid.VerticalSize);

            for(int x = 0; x < maxX; ++x)
            {
                for (int y = 0; y < maxY; ++y)
                {
                    newGrid.SetTile(x, y, Grid.GetTile(x, y));
                }
            }

            Grid = newGrid;

            System.GC.Collect();
        }

        public override void FromJSON(JSONNode json)
        {
            Name = json["name"];

            Grid = new Grid(0, 0);

            Grid.FromJSON(json["grid"]);
        }

        public override JSONNode ToJSON()
        {
            JSONNode json;

            json = JSON.Parse((Resources.Load<TextAsset>("Data/JSON/Templates/Map").text));

            json["name"] = Name;
            json.Add("players", GameController.Instance.Game.Players.ToJSON());
            json["grid"] = Grid.ToJSON();

            return json;
        }

        public string   Name;
        public Grid     Grid;
    }
}
