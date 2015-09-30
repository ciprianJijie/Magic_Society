using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

namespace MS.Model
{
    public class Trait : ModelElement, IEnumerable<Modifier>
    {
        public string Description;

        protected List<Modifier> m_Modifiers;

        public Trait()
        {
            m_Modifiers = new List<Modifier>();
        }

        public override void FromJSON(JSONNode json)
        {
            base.FromJSON(json);

            Description = json["description"];

            foreach (JSONNode node in json["modifiers"].AsArray)
            {
                Modifier modifier;

                modifier = Modifier.Create(node["name"]);
                modifier.FromJSON(node);
                m_Modifiers.Add(modifier);
            }
        }

        public override JSONNode ToJSON()
        {
            JSONNode root;
            JSONArray array;

            root = base.ToJSON();
            array = new JSONArray();

            root.Add("description", Description);

            foreach (Modifier modifier in m_Modifiers)
            {
                array.Add(modifier.ToJSON());
            }

            root.Add("modifiers", array);

            return root;
        }

        public IEnumerator<Modifier> GetEnumerator()
        {
            return m_Modifiers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_Modifiers.GetEnumerator();
        }

        public override string ToString()
        {
            string modifiers;

            modifiers = "";

            foreach (Modifier modifier in m_Modifiers)
            {
                modifiers += modifier + ", ";
            }

            return string.Format("{0} [{1}]", Name, modifiers);
        }
    }
}
