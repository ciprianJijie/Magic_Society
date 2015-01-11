using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MS.Model
{
	public class HexGrid : ModelElement
	{
		private Tile[,] m_tiles;

		public int HorizontalSize;
		public int VerticalSize;

		public HexGrid(SimpleJSON.JSONNode json)
		{
			FromJSON(json);
		}

		public Tile this[int x, int y]
		{
			get
			{
				return m_tiles[x,y];
			}

			set
			{
				m_tiles[x,y] = value;
			}
		}

		public override void FromJSON(SimpleJSON.JSONNode json)
		{
			SimpleJSON.JSONArray 	gridRow;
			SimpleJSON.JSONArray 	gridColumn;

			HorizontalSize 	= 	json["size"]["horizontal"].AsInt;
			VerticalSize 	= 	json["size"]["vertical"].AsInt;

			m_tiles = new Tile[HorizontalSize, VerticalSize];

			gridRow = json["tiles"].AsArray;

			for (int x = 0; x < gridRow.Count; ++x)
			{
				gridColumn = json["tiles"][x].AsArray;

				for (int y = 0; y < gridColumn.Count; ++y)
				{
					m_tiles[x, y] = new Tile(json["tiles"][x][y]);
				}
			}
		}

		public override SimpleJSON.JSONNode ToJSON()
		{
			SimpleJSON.JSONNode json;

			json = new SimpleJSON.JSONNode();

			return json;
		}
	}
}