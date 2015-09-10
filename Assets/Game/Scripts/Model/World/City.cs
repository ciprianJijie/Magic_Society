using UnityEngine;
using SimpleJSON;
using System.Collections.Generic;

namespace MS.Model
{
	public class City : OwnableMapElement, IResourceCollector, IUpkeepMaintained
	{
        public static readonly int FOOD_PER_POPULATION                  =   2;
        public static readonly int PRODUCTION_PER_POPULATION            =   2;
        public static readonly int RESEARCH_PER_POPULATION              =   2;
        public static readonly int GOLD_PER_POPULATION                  =   4;
        public static readonly float FOOD_CONSUMPTION_PER_POPULATION    =   1.0f;

        public string RealName;

        // Events
        public Events.CityEvent OnPopulationGrow    =   Events.DefaultAction;
        public Events.CityEvent OnPopulationDecrese =   Events.DefaultAction;

        protected int m_Population;
        protected int m_FoodStored;
        protected List<Vector2> m_TilesUnderControl;

        // Recollection
        protected int m_PopulationInFood;
        protected int m_PopulationInProduction;
        protected int m_PopulationInResearch;
        protected int m_PopulationInGold;

        protected Kingdom.BuildingQueue m_BuildingQueue;

        protected List<MS.Model.Kingdom.Building> m_Buildings;

        public int Population
        {
            get
            {
                return m_Population;
            }
        }

        public int Food
        {
            get
            {
                return m_FoodStored;
            }

            set
            {
                m_FoodStored = value;
            }
        }

        public int Production
        {
            get
            {
                return 0;
            }
        }

        public Kingdom.BuildingQueue BuildingQueue
        {
            get
            {
                return m_BuildingQueue;
            }
        }

        public City()
        {
            m_FoodStored            =   0;
            m_Population            =   1;
            m_TilesUnderControl     =   new List<Vector2>();
            m_Buildings             =   new List<Kingdom.Building>();
            m_BuildingQueue         =   new Kingdom.BuildingQueue(this);

            m_TilesUnderControl.Add(new Vector2(X, Y));
            Build("Town Hall");
        }

        public int CalculateFoodForNextPopulationUnit(int currentPopulation)
        {
            return Mathf.RoundToInt(20f + (4f * currentPopulation) * Mathf.Pow(1.25f, currentPopulation));
        }

        public int CalculateBuyableTileCount()
        {
            return m_TilesUnderControl.Count - m_Population;
        }

        public void PayUpkeepCosts()
        {
            int foodForPeople;

            foodForPeople = Mathf.RoundToInt(m_Population * FOOD_CONSUMPTION_PER_POPULATION);

            if (m_FoodStored < foodForPeople)
            {
                UnityEngine.Debug.Log("Not enough food (" + m_FoodStored + ") to feed population (" + m_Population + ") requiring " + foodForPeople + " food units.");

                m_FoodStored = CalculateFoodForNextPopulationUnit(m_Population - 1) - (foodForPeople - m_FoodStored);

                DecreasePopulation(1);
            }
            else
            {
                m_FoodStored = m_FoodStored - foodForPeople;
            }
        }

        public void CollectResources()
        {
            int food;
            int production;
            int research;
            int gold;
            int foodToGrow;

            // Population
            food        =   m_PopulationInFood * FOOD_PER_POPULATION;
            production  =   m_PopulationInProduction * PRODUCTION_PER_POPULATION;
            research    =   m_PopulationInResearch * RESEARCH_PER_POPULATION;
            gold        =   m_PopulationInGold * GOLD_PER_POPULATION;
            foodToGrow  =   CalculateFoodForNextPopulationUnit(m_Population);

            // Tiles
            foreach (Vector2 tilePosition in m_TilesUnderControl)
            {
                food        +=  Game.Instance.Resources.CalculateFoodGeneration(tilePosition);
                production  +=  Game.Instance.Resources.CalculateProductionGeneration(tilePosition);
                research    +=  Game.Instance.Resources.CalculateResearchGeneration(tilePosition);
                gold        +=  Game.Instance.Resources.CalculateGoldGeneration(tilePosition);
            }

            // Buildings
            foreach (Kingdom.Building building in m_Buildings)
            {
                building.OnRecollection();
            }

            // Food
            m_FoodStored += food;

            if (m_FoodStored >= foodToGrow)
            {
                m_FoodStored = Mathf.RoundToInt((m_Population + 1) * FOOD_CONSUMPTION_PER_POPULATION);  // Enough to pay in the Upkeep and stay at 0
                GrowPopulation(1);
            }

            // Production
            m_BuildingQueue.AddProduction(production);

            // Gold
            Owner.Gold += gold;

            // Research
            Owner.Research += research;
        }

        public Kingdom.Building Build(string type)
        {
            Kingdom.Building building;

            building        =   Kingdom.Building.Factory.Create(type);
            building.Owner  =   Owner;
            building.City   =   this;

            m_Buildings.Add(building);

            return building;
        }

        public void GrowPopulation(int amount)
        {
            m_Population += amount;

            m_PopulationInFood += amount;

            OnPopulationGrow(this);
        }

        public void DecreasePopulation(int amount)
        {
            int newPopulation;
            int diff;

            newPopulation   =   Mathf.Max(1, m_Population - amount);
            diff            =   m_Population - newPopulation;
            m_Population    =   newPopulation;

            while (diff > 0)
            {
                if (m_PopulationInFood > 0)
                {
                    m_PopulationInFood--;
                }
                else if (m_PopulationInProduction > 0)
                {
                    m_PopulationInProduction--;
                }
                else if (m_PopulationInResearch > 0)
                {
                    m_PopulationInResearch--;
                }
                else if (m_PopulationInGold > 0)
                {
                    m_PopulationInGold--;
                }

                diff--;
            }

            OnPopulationDecrese(this);
        }

        public override void FromJSON(JSONNode json)
        {
            base.FromJSON(json);
            RealName = json["real_name"];

            foreach (JSONNode node in json["tiles"].AsArray)
            {
                Vector2 tile;

                tile.x = node["x"].AsInt;
                tile.y = node["y"].AsInt;

                m_TilesUnderControl.Add(tile);
            }
            
            if (json["building_queue"] != null)
            {
                m_BuildingQueue.City = this;
                m_BuildingQueue.FromJSON(json["building_queue"]);
            }
        }

        public override JSONNode ToJSON()
        {
            JSONNode root;
            JSONArray tilesArray;
            JSONClass tileNode;

            root = base.ToJSON();
            root.Add("real_name", new JSONData(RealName));

            tilesArray = new JSONArray();

            foreach (Vector2 tile in m_TilesUnderControl)
            {
                tileNode = new JSONClass();

                tileNode.Add("x", new JSONData(tile.x));
                tileNode.Add("y", new JSONData(tile.y));
            }

            root.Add("tiles", tilesArray);
            root.Add("building_queue", m_BuildingQueue.ToJSON());

            return root;
        }
	}
}
