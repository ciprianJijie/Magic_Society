using UnityEngine;
using SimpleJSON;
using System.Collections.Generic;
using MS.Model;

namespace MS
{
	public class Game : ModelElement
	{
        protected Map               m_Map;
        protected Players           m_Players;
        protected Turns             m_Turns;
        protected Model.Resources   m_Resources;

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

        public Turns Turns
        {
            get
            {
                return m_Turns;
            }
        }

        public Model.Resources Resources
        {
            get
            {
                return m_Resources;
            }
        }

        public Game()
        {
            m_Map       =   new Map();
            m_Players   =   new Players();
            m_Turns     =   new Turns(m_Players);
            m_Resources =   new Model.Resources();
        }

        public void New(string mapName, int numPlayers, int humanPlayers)
        {
            string      filePath;
            JSONNode    json;

            filePath    =   Path.ToScenario(mapName);
            json        =   Path.FileToJSON(filePath);

            m_Players.FromJSON(json["players"]);
            m_Map.FromJSON(json);

            m_Turns = new Turns(m_Players);
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

            m_Turns = new Turns(m_Players);
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