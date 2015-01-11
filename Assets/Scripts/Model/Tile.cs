using System.Collections;
using System.Collections.Generic;

namespace MS.Model
{
	public class Tile : ModelElement
	{
		public enum TerrainType { None, Grass, Water, Forest, Mountain }

		private TerrainType m_type;

		public TerrainType Type
		{
			get { return m_type; }
		}

		public Tile(SimpleJSON.JSONNode json)
		{
			FromJSON(json);
		}

		public override void FromJSON(SimpleJSON.JSONNode json)
		{
			string type;

			type = json["terrain"].Value;

			if (type == "Grass")
			{
				m_type = TerrainType.Grass;
			}
			else if (type == "Water")
			{
				m_type = TerrainType.Water;
			}
			else if (type == "Forest")
			{
				m_type = TerrainType.Forest;
			}
			else if (type == "Mountain")
			{
				m_type = TerrainType.Mountain;
			}
		}

		public override SimpleJSON.JSONNode ToJSON()
		{
			SimpleJSON.JSONNode json;

			json = new SimpleJSON.JSONNode();
			json["type"] = m_type.ToString();

			return json;
		}

		public override string ToString()
		{
			return m_type.ToString();
		}
	}
}