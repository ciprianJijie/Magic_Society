using UnityEngine;
using SimpleJSON;
using System;

namespace MS
{
    public class MapElement : ModelElement
    {
        // Attributes
        public string Name;
        // ---

        public MapElement()
        {
            // Defaul values
            Name = "No name";
        }

        public override void FromJSON(JSONNode json)
        {
            Name = json["name"];
        }

        public override JSONNode ToJSON()
        {
            JSONNode json = JSON.Parse(Resources.Load<TextAsset>("Data/JSON/Templates/MapElement").text);
            json["name"] = Name;

            return json;
        }

        // Factory Methods

        public static MapElement Create(string name)
        {
            MapElement element;

            if (name == "Forest")
            {
                element = new Forest();
            }
            else
            {
                element = new MapElement();
            }

            return element;
        }

        // ---
    }
}
