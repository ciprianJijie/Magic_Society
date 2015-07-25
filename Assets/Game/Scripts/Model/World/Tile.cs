using UnityEngine;
using SimpleJSON;

namespace MS
{
	public class Tile : ModelElement
	{
		// Terrain Type
		public enum Terrain { Fertile, Barren, Desert, Frozen, Volcanic }
		// ---
        
        public int          X;
        public int          Y;

        public int 			Height;
        public Terrain 		TerrainType;
        public MapElement 	Element;

        public override void FromJSON(JSONNode json)
		{
            X               =   json["x"].AsInt;
            Y               =   json["y"].AsInt;
            Height 			= 	json["height"].AsInt;
            TerrainType 	= 	EnumUtils.ParseEnum<Terrain>(json["terrain"]);
            
            if (json["element"] != null)
            {
                Element     = 	ModelElement.FromJSON<MapElement>(json["element"]);
            }
        }

		public override JSONNode ToJSON()
		{
            JSONNode root = JSON.Parse("{}");
            
            root.Add("x", new JSONData(X));
            root.Add("y", new JSONData(Y));
            root.Add("height", new JSONData(Height));
            root.Add("terrain", TerrainType.ToString());
            
            if (Element != null)
            {
                root.Add("element", Element.ToJSON());
            }
            return root;
        }
    }
}
