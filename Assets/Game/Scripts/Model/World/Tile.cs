using UnityEngine;
using SimpleJSON;

namespace MS
{
	public class Tile : ModelElement
	{
		// Terrain Type
		public enum Terrain { Fertile, Barren, Desert, Frozen, Volcanic }
		// ---

        public int 			Height;
        public Terrain 		TerrainType;
        public MapElement 	Element;

        public override void FromJSON(JSONNode json)
		{
            Height 			= 	json["height"].AsInt;
            TerrainType 	= 	EnumUtils.ParseEnum<Terrain>(json["terrain"]);
            Element 		= 	ModelElement.FromJSON<MapElement>(json["element"]);
        }

		public override JSONNode ToJSON()
		{
            JSONNode node = JSON.Parse(Resources.Load<TextAsset>("Data/JSON/Templates/Tile").text);

			node["height"].AsInt 	= 	Height;
            node["terrain"] 		= 	TerrainType.ToString();

            if (Element != null)
                node["element"] 	= 	Element.ToJSON();

            return node;
        }
    }
}
