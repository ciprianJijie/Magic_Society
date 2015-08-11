using UnityEngine;
using SimpleJSON;
using System.Collections.Generic;

namespace MS.Model
{
	public class City : OwnableMapElement
	{
        protected List<MS.Model.Kingdom.CityDistrict> m_Districts;

        public City()
        {
            m_Districts = new List<MS.Model.Kingdom.CityDistrict>();
        }

        public void Initialize()
        {
            ExpandCity(X, Y);

            UnityEngine.Debug.Log("City initialized with district " + m_Districts[0].Name);
        }

        public bool ExpandCity(Vector2 position)
        {
            return ExpandCity((int)position.x, (int)position.y);
        }

        public bool ExpandCity(int x, int y)
        {
            if (CanExpand(x, y))
            {
                MS.Model.Kingdom.CityDistrict district;
                string name;
                
                name        =   MS.Generators.NameGenerator.RandomDistrictName();
                district    =   new MS.Model.Kingdom.CityDistrict(Owner, x, y, name);
                
                m_Districts.Add(district);

                return true;
            }
            
            return false;
        }

        public bool CanExpand(Vector2 position)
        {
            return CanExpand((int)position.x, (int)position.y);
        }

        public bool CanExpand(int x, int y)
        {
            return IsAdjacent(x, y) && TileSuitableForBuilding(x, y);
        }

        protected bool IsAdjacent(Vector2 position)
        {
            return IsAdjacent((int)position.x, (int)position.y);
        }

        protected bool IsAdjacent(int x, int y)
        {
            if (m_Districts.Count == 0)
            {
                return true;
            }
            
            foreach (MS.Model.Kingdom.CityDistrict district in m_Districts)
            {
                if (Mathf.Abs(district.X - x) < 2 && Mathf.Abs(district.Y - y) < 2)
                {
                    return true;
                }
            }
            
            return false;
        }

        protected bool TileSuitableForBuilding(Vector2 position)
        {
            return TileSuitableForBuilding((int)position.x, (int)position.y);
        }

        protected bool TileSuitableForBuilding(int x, int y)
        {
            return GameController.Instance.Game.Map.Grid.GetElement(x, y) == null;
        }

        public override void FromJSON(JSONNode json)
        {
            MS.Model.Kingdom.CityDistrict district;

            base.FromJSON(json);

            foreach (JSONNode node in json["districts"].AsArray)
            {
                district = new MS.Model.Kingdom.CityDistrict();

                district.FromJSON(node);

                m_Districts.Add(district);
            }
        }

        public override JSONNode ToJSON()
        {
            JSONNode json;
            JSONArray array;

            json = base.ToJSON();

            array = new JSONArray();

            foreach (MS.Model.Kingdom.CityDistrict district in m_Districts)
            {
                array.Add(district.ToJSON());
            }

            json.Add("districts", array);

            return json;
        }
	}
}
