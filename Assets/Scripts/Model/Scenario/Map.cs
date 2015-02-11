using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System.Linq;

namespace MS.Model
{
	public class Map : ModelElement
	{
		public Map()
		{
			
		}

		public Map(SimpleJSON.JSONNode json)
		{
			FromJSON(json);
		}

		public override void FromJSON(SimpleJSON.JSONNode json)
		{
			Grid 		= 	new HexGrid(json["grid"]);
			Resources 	=	new List<GameResource>(json["resources"].Count);
			Elements 	= 	new List<MapElement>(json["elements"].Count);

			foreach (JSONNode resourceNode in json["resources"].AsArray)
			{
				Resources.Add(GameResource.Create(resourceNode));
			}

            foreach (JSONNode element in json["elements"].AsArray)
            {
                Elements.Add(MapElement.Create(element));
            }
		}

		public override SimpleJSON.JSONNode ToJSON()
		{
			SimpleJSON.JSONNode json;

			json = new SimpleJSON.JSONNode();

			return json;
		}

		public GameResource GetResource(string name)
		{
			return Resources.Where(i => i.Name == name) as GameResource;
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
