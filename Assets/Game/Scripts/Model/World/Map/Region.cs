using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

namespace MS.Model.Map
{
    public class Region : ModelElement, IEnumerable<Area>
    {
        public Vector2          MapPosition; 

        protected List<Area>    m_Areas;
        protected Area          m_Capital;

        public Region()
        {
            m_Areas = new List<Area>(7);

            for (int i = 0; i < m_Areas.Count; i++)
            {
                m_Areas[i] = new Area();
            }

            m_Capital = m_Areas[0];
        }

        public override void FromJSON(JSONNode json)
        {
            base.FromJSON(json);

            JSONArray array;

            array = json["areas"].AsArray;

            m_Capital.FromJSON(array[0]);

            for (int i = 1; i < array.Count; i++)
            {
                m_Areas[i].FromJSON(array[i]);
            }
        }

        public override JSONNode ToJSON()
        {
            JSONNode root;
            JSONArray array;

            root = base.ToJSON();
            array = new JSONArray();

            root.Add("capital", m_Capital.ToJSON());

            for (int i = 1; i < m_Areas.Count; i++)
            {
                array.Add(m_Areas[i].ToJSON());
            }

            root.Add("areas", array);

            return root;
        }

        public IEnumerator<Area> GetEnumerator()
        {
            return m_Areas.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_Areas.GetEnumerator();
        }
    }
}
