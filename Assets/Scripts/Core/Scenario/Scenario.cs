using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using SimpleJSON;

namespace MS.Model
{
    public class Scenario
    {
        public Scenario()
        {

        }

        public void Load(string filePath)
        {
            string      jsonText;
            JSONNode    root;
            JSONArray   playersArray;

            if (System.IO.File.Exists(filePath) == false)
            {
                throw new MS.NoFileFound(filePath);
            }

            MS.Debug.Core.Log("Parsing file " + filePath);

            jsonText = System.IO.File.ReadAllText(filePath);

            root = JSON.Parse(jsonText);

            if (root == null)
            {
                throw new MS.FailedToParseJSON(filePath);
            }

            Name            =   root["name"];
            playersArray    =   root["players"].AsArray;
            Players         =   new MS.Model.Player[playersArray.Count];

            JSONNode node;
            string playerName;
            string playerType;

            for (int index = 0; index < playersArray.Count; ++index)
            {
                node        =   playersArray[index];
                playerName  =   node["name"].Value;
                playerType  =   node["type"].Value;

                if (playerType == "Human")
                {
                    Players[index] = new Model.HumanPlayer(playerName);
                }
                else if (playerType == "AI")
                {
                    Players[index] = new Model.AIPlayer(playerName);
                }
            }

            Map = new Map(root["map"]);

            MS.Debug.Core.Log("Loaded scenario " + Name);
            MS.Debug.Core.Log(Map);
        }

        public string Name;
        public Model.Player[] Players;
        public Map Map;
    }
}
