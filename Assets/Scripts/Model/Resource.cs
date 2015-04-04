using UnityEngine;
using SimpleJSON;

namespace MS
{
    public class Resource : Model.ModelElement
    {
        public Resource()
        {

        }

        public override void FromJSON(JSONNode json)
        {

        }

        public override JSONNode ToJSON()
        {
            JSONNode json;

            json = new JSONNode();

            return json;
        }

        public string Name;
        public string Description;
    }
}
