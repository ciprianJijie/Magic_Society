using System;
using SimpleJSON;

namespace MS.Model
{
    public class AIPlayer : Player
    {
        public AIPlayer(JSONNode json)
            : base(json)
        {
            MS.Debug.Core.Log("Created AI player " + Name);
        }

        public override void FromJSON(SimpleJSON.JSONNode node)
        {
            this.m_name = node["name"];
        }

        public override SimpleJSON.JSONNode ToJSON()
        {
            throw new NotImplementedException();
        }
    }
}

