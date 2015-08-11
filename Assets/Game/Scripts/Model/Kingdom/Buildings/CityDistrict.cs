using UnityEngine;
using SimpleJSON;
using System.Collections.Generic;

namespace MS.Model.Kingdom
{
    public class CityDistrict : OwnableMapElement
    {
        protected Building[]    m_Buildings;

        public CityDistrict()
        {
            m_Buildings = new Building[6];
        }

        public CityDistrict(Player owner, int x, int y, string name)
        {
            Owner   =   owner;
            X       =   x;
            Y       =   y;
            Name    =   name;
            m_Buildings =   new Building[6];
        }

        public T Build<T>() where T: Building, new()
        {
            T       building;
            int     indexToBuild;

            building        =   null;
            indexToBuild    =   SearchIndexToBuildInto();

            if (indexToBuild > -1)
            {
                building = new T();

                m_Buildings[indexToBuild] = building;
            }

            return building;
        }

        public bool Build(Building building)
        {
            int indexToBuild;

            indexToBuild = SearchIndexToBuildInto();

            if (indexToBuild > -1)
            {
                m_Buildings[indexToBuild] = building;

                return true;
            }
            return false;
        }

        public bool Demolish<T>() where T: Building
        {
            int index;

            index = SearchBuildingIndex<T>();

            if (index > -1)
            {
                m_Buildings[index] = null;
                return true;
            }
            return false;
        }

        public bool HasAvailableSpace()
        {
            for (int index = 0; index < m_Buildings.Length; ++index)
            {
                if (m_Buildings[index] == null)
                {
                    return true;
                }
            }
            return false;
        }

        protected int SearchIndexToBuildInto()
        {
            for (int index = 0; index < m_Buildings.Length; ++index)
            {
                if (m_Buildings[index] == null)
                {
                    return index;
                }
            }

            return -1;
        }

        public T SearchBuilding<T>() where T: Building
        {
            int index;

            index = SearchBuildingIndex<T>();

            if (index > -1)
            {
                return m_Buildings[index] as T;
            }

            return null;
        }

        public int SearchBuildingIndex<T>() where T:Building
        {
            for (int index = 0; index < m_Buildings.Length; ++index)
            {
                if (m_Buildings[index] is T)
                {
                    return index;
                }
            }

            return -1;
        }

        public override void FromJSON(JSONNode json)
        {
            base.FromJSON(json);

            Name = json["name"];

            Building building;

            foreach (JSONNode node in json["buildings"].AsArray)
            {
                building = Building.Factory.Create(node["name"]);
                building.FromJSON(node);

                Build(building);
            }
        }

        public override JSONNode ToJSON()
        {
            JSONNode json;
            JSONArray array;

            json = base.ToJSON();
            array = new JSONArray();

            json.Add("name", new JSONData(Name));

            foreach (Building building in m_Buildings)
            {
                array.Add(building.ToJSON());
            }

            json.Add("buildings", array);

            return json;
        }
    }
}

