using UnityEngine;
using SimpleJSON;
using System.Collections.Generic;
using MS.Model;

namespace MS
{
	public class Game : ModelElement
	{
        protected Map       m_Map;
        protected Players   m_Players;

        public Map Map
        {
            get
            {
                return m_Map;
            }
        }

        public Players Players
        {
            get
            {
                return m_Players;
            }
        }

        public Game()
        {
            m_Map       =   new Map();
            m_Players   =   new Players();
        }

        public void New(string mapName, int numPlayers, int humanPlayers)
        {
            string      filePath;
            JSONNode    json;

            filePath    =   Path.ToScenario(mapName);
            json        =   Path.FileToJSON(filePath);

            m_Map.FromJSON(json);

            for (int i = 0; i < numPlayers; i++)
            {
                if (i < humanPlayers)
                {
                    m_Players.AddPlayer(new HumanPlayer("Human Player " + i));
                }
                else
                {
                    m_Players.AddPlayer(new AIPlayer("AI Player " + i));
                }
            }
        }

        public void Save(string fileName)
        {
            string filePath;

            filePath = Path.ToSaveGame(fileName);

            Path.JSONToFile(ToJSON(), filePath);
        }

        public void Load(string fileName)
        {
            string      filePath;
            JSONNode    json;

            filePath    =   Path.ToSaveGame(fileName);
            json        =   Path.FileToJSON(filePath);

            FromJSON(json);
        }

        public override void FromJSON(JSONNode json)
        {
            m_Players.FromJSON(json["players"]);
            m_Map.FromJSON(json["map"]);
        }

        public override JSONNode ToJSON()
        {
            JSONNode json = new JSONNode();

            json.Add("players", m_Players.ToJSON());
            json.Add("map", m_Map.ToJSON());

            return json;
        }
	}
}