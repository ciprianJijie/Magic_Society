using UnityEngine;
using SimpleJSON;

namespace MS.Model
{
    public class GameResource : ModelElement
    {
        public GameResource()
        {
            m_name          =   "No name";
            m_description   =   "No description";
        }

        public GameResource(string name, string description)
        {
            m_name          =   name;
            m_description   =   description;
        }

        public GameResource(JSONNode json)
        {
            FromJSON(json);
        }

        public static GameResource Create(JSONNode json)
        {
            GameResource resource = new GameResource(json);

            MS.Debug.Core.Log("Created resource " + resource);

            return resource;
        }

        public override void FromJSON(JSONNode json)
        {
            m_name          =   json["name"].Value;
            m_description   =   json["description"].Value;
        }

        public override JSONNode ToJSON()
        {
            JSONNode json = new JSONNode();

            json["name"] = m_name;
            json["description"] = m_description;

            return json;
        }

        public override string ToString()
        {
            return Name;
        }

        // Properties

        public string Name
        {
            get
            {
                return m_name;
            }
        }

        public string Description
        {
            get
            {
                return m_description;
            }
        }

        // Attributes

        protected string m_name;
        protected string m_description;
    }
}
