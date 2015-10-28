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
        protected NobleHouses           m_NobleHouses;

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
            m_Resources     =   new Resources();
            m_Schemes       =   new Kingdom.Schemes();
            m_Personalities =   new Personalities();
            m_NobleHouses   =   new NobleHouses();

            m_Instance  =   this;
        }

        public void New(int worldSize, int aiPlayers)
        {
            JSONNode schemesJSON;

            schemesJSON = Path.FileToJSON(Path.ToData("Schemes.json"));

            // Generate Players
            m_Players.AddPlayer(new HumanPlayer("PLAYER_HUMAN"));

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
            GenerateCities(m_NobleHouses);
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
                if (player is NeutralPlayer == false)
                {
                    // Main house

                    player.MainHouse = m_NobleHouses.GenerateRandom(player);

                    // Vassal houses

                    NobleHouse vassalHouse;

                    for (int i = 0; i < 2; i++)
                    {
                        vassalHouse = m_NobleHouses.GenerateRandom(player);
                        vassalHouse.ChiefHouse = player.MainHouse;
                    }
                }                
            }

            int neutralHousesCount;
            
            neutralHousesCount = Mathf.FloorToInt(m_World.TileCount * 0.6f);

            for (int i = 0; i < neutralHousesCount; i++)
            {
                m_NobleHouses.GenerateRandom(m_Players.NeutralPlayer);
            }
        }

        protected void GenerateCities(IEnumerable<NobleHouse> houses)
        {
            List<Vector3>       positions;
            Vector3             position;
            int                 index;
            World.Region        region;
            City                city;

            positions = new List<Vector3>(Hexagon.CalculateTilesForRange(m_World.Range));
            positions.Remove(Vector3.zero);

            foreach (NobleHouse house in houses)
            {
                if (/*house.ChiefHouse == null*/ true)
                {
                    index                       =   Random.Range(0, positions.Count);
                    position                    =   positions[index];
                    region                      =   m_World.GetRegion(position);
                    region.ChiefHouse           =   house;
                    city                        =   new City();
                    city.RealName               =   Generators.NameGenerator.RandomCityName();
                    city.Owner                  =   house.Owner;
                    city.ChiefHouse             =   house;
                    region.CapitalArea.Element  =   city;

                    positions.Remove(position);                    
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