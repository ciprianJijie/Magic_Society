using UnityEngine;
using SimpleJSON;

namespace MS.Model
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

        public override void FromJSON(JSONNode json)
		{
            X               =   json["x"].AsInt;
            Y               =   json["y"].AsInt;
            Height 			= 	json["height"].AsInt;
            TerrainType 	= 	EnumUtils.ParseEnum<Terrain>(json["terrain"]);
        }

		public override JSONNode ToJSON()
		{
            JSONNode root = JSON.Parse("{}");
            
            root.Add("x", new JSONData(X));
            root.Add("y", new JSONData(Y));
            root.Add("height", new JSONData(Height));
            root.Add("terrain", TerrainType.ToString());

            return root;
        }
    }
}
