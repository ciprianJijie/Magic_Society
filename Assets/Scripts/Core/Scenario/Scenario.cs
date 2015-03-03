using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using SimpleJSON;

namespace MS.Model
{
    public class Scenario : ModelElement
    {
        public Scenario()
        {
            
        }

        public Scenario(JSONNode node)
        {
            FromJSON(node);
        }

        public override void FromJSON(JSONNode node)
        {
            JSONArray playersArray;

            Name            =   node["name"];

            Map = new Map();
            Map.FromJSON(node["map"]);

            playersArray    =   node["players"].AsArray;
            Players         =   new Player[playersArray.Count];

            for (int playerIndex = 0; playerIndex < playersArray.Count; ++playerIndex)
            {
                Players[playerIndex] = Player.Create(playersArray[playerIndex]);
            }
        }

        public override JSONNode ToJSON()
        {
            throw new System.NotImplementedException();
        }

        public string Name;
        public Model.Player[] Players;
        public Map Map;
    }
}
