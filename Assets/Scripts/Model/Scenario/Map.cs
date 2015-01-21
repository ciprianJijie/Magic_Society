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
			Grid = new HexGrid(json["grid"]);

            m_elements = new List<MapElement>(json["elements"].Count);

            foreach (JSONNode element in json["elements"].AsArray)
            {
                m_elements.Add(MapElement.Create(element));
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

        private List<MapElement> m_elements;


        #endregion
	}
}