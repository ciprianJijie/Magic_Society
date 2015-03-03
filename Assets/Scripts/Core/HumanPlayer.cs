using System;
using SimpleJSON;

namespace MS.Model
{
    public class HumanPlayer : Player
    {
        public HumanPlayer(JSONNode json)
            : base(json)
        {
            MS.Debug.Core.Log("Created human player " + Name);
        }

        public override void FromJSON(JSONNode node)
        {
            this.m_name = node["name"];

            foreach (JSONNode resourceNode in node["resources"].AsArray)
            {
                AddResource(resourceNode["name"], resourceNode["amount"].AsInt);
            }
        }

        public override JSONNode ToJSON()
        {
            throw new NotImplementedException();
        }
    }
}

