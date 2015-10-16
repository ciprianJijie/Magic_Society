using UnityEngine;
using SimpleJSON;
using System.Collections.Generic;
using MS.Model;

namespace MS.Model
{
	public class Game : ModelElement
	{
        protected World.World           m_World;
        protected Players               m_Players;
        protected Turns                 m_Turns;
        protected Resources             m_Resources;
        protected Kingdom.Schemes       m_Schemes;
        protected Personalities         m_Personalities;

        public Model.World.World World
        {
            get
            {
                return m_World;
            }

            set
            {
                m_World = value;
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

        public Model.Kingdom.Schemes Schemes
        {
            get
            {
                return m_Schemes;
            }
        }

        public Model.Personalities Personalities
        {
            get
            {
                return m_Personalities;
            }
        }

        // Singleton
        private static Game m_Instance;

        public static Game Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Game();
                }

                return m_Instance;
            }
        }
        // ---

        public Game()
        {
            m_Players       =   new Players();
            m_Turns         =   new Turns(m_Players);
            m_Resources     =   new Resources();
            m_Schemes       =   new Kingdom.Schemes();
            m_Personalities =   new Personalities();

            m_Instance  =   this;
        }

        public void New(int worldSize, int aiPlayers)
        {
            JSONNode schemesJSON;

            schemesJSON = Path.FileToJSON(Path.ToData("Schemes.json"));

            // Generate Players
            m_Players.AddPlayer(new HumanPlayer("PLAYER_HUMAN"));
            m_Players.AddPlayer(new NeutralPlayer());

            for (int i = 0; i < aiPlayers; ++i)
            {
                m_Players.AddPlayer(new AIPlayer("PLAYER_AI_" + i));
            }

            m_Turns = new Turns(m_Players);

            // Generate World
            m_World = new World.World(worldSize);

            m_World.GenerateRandom();
            
            m_Schemes.FromJSON(schemesJSON);
            GenerateStartingPersonalities(m_Players);
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
            JSONNode    schemesJSON;

            filePath    =   Path.ToSaveGame(fileName);
            json        =   Path.FileToJSON(filePath);
            schemesJSON =   Path.FileToJSON(Path.ToData("Schemes.json"));

            m_Schemes.FromJSON(schemesJSON);
            FromJSON(json);
        }

        protected void GenerateStartingPersonalities(IEnumerable<Player> players)
        {
            foreach (Player player in players)
            {
                for (int i = 0; i < Model.Personalities.STARTING_PERSONALITIES; i++)
                {
                    m_Personalities.CreateRandom(player);
                }
            }
        }

        public override void FromJSON(JSONNode json)
        {
            m_Players.FromJSON(json["players"]);
            m_World.FromJSON(json["world"]);

            m_Turns = new Turns(m_Players);
        }

        public override JSONNode ToJSON()
        {
            JSONNode json = new JSONNode();

            json.Add("players", m_Players.ToJSON());
            json.Add("world", m_World.ToJSON());

            return json;
        }
	}
}