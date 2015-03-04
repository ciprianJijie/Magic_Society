using UnityEngine;
using SimpleJSON;

namespace MS
{
    public class Player : Model.ModelElement
    {
        public Player()
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
    }
}
