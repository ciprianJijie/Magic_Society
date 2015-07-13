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

        public override void FromJSON(JSONNode json)
        {
            Name = json["name"];
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

        public string Name;
        protected Grid Tiles;
    }
}
