using UnityEngine;
using SimpleJSON;

namespace MS.Model
{
    public abstract class Resource : ModelElement
    {
        public string Name;

        public override void FromJSON(JSONNode json)
        {
            Name = json["name"];
        }

        public override JSONNode ToJSON()
        {
            JSONClass root;

            root = new JSONClass();

            root.Add("name", Name);

            return root;
        }
    }
}
