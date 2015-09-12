using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

namespace MS.Model.Kingdom
{
    /// <summary>
    /// Information about all buildings, units, technologies, etc that exist in the game.
    /// </summary>
    public class Schemes : ModelElement, IEnumerable<Building>
    {
        protected List<Building> m_BuildingSchemes;

        public Schemes()
        {
            m_BuildingSchemes = new List<Building>();
        }

        public override void FromJSON(JSONNode json)
        {
            JSONArray buildings;

            buildings = json["buildings"].AsArray;

            foreach (JSONNode node in buildings)
            {
                Building building;

                building = Building.Factory.Create(node["name"]);
                building.FromJSON(node);

                m_BuildingSchemes.Add(building);
            }
        }        

        public override JSONNode ToJSON()
        {
            JSONNode root;
            JSONArray buildings;

            root = base.ToJSON();
            buildings = new JSONArray();

            foreach (Building building in m_BuildingSchemes)
            {
                buildings.Add(building.ToJSON());
            }

            root.Add("buildings", buildings);

            return root;
        }

        public IEnumerator<Building> GetEnumerator()
        {
            return m_BuildingSchemes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
