using SimpleJSON;

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
            JSONNode json = new JSONNode();

            json["name"] = Name;

            json["grid"] = Tiles.ToJSON();

            return json;
        }

        public string Name;
        protected Grid Tiles;
    }
}
