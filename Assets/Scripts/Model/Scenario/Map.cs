using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

namespace MS.Model
{
	public class Map : ModelElement
	{
		public Map(SimpleJSON.JSONNode json)
		{
			FromJSON(json);
		}

		public override void FromJSON(SimpleJSON.JSONNode json)
		{
			Grid 		= 	new HexGrid(json["grid"]);
            Elements 	= 	new List<MapElement>(json["elements"].Count);
			Resources 	=	new List<GameResource>(json["resources"].Count);

            foreach (JSONNode element in json["elements"].AsArray)
            {
                Elements.Add(MapElement.Create(element));
            }

			foreach (JSONNode resourceNode in json["resources"].AsArray)
			{
				Resources.Add(GameResource.Create(resourceNode));
			}
		}

		public override SimpleJSON.JSONNode ToJSON()
		{
			SimpleJSON.JSONNode json;

			json = new SimpleJSON.JSONNode();

			return json;
		}

		public override string ToString()
		{
			string str = "Map : \n";

			for (int y = 0; y < Grid.VerticalSize; ++y)
			{
				str += "[";
				for (int x = 0; x < Grid.HorizontalSize; ++x)
				{
					str += Grid[x, y].ToString() + ",";
				}
				str += "]\n";
			}

			return str;
		}

        #region Attributes

        public HexGrid Grid;

        public List<MapElement> 	Elements;
		public List<GameResource>	Resources;


        #endregion
	}
}
