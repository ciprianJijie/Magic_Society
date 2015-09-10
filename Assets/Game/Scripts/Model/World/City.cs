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

        public City()
        {
            m_FoodStored            =   0;
            m_Population            =   1;
            m_TilesUnderControl     =   new List<Vector2>();
            m_Buildings             =   new List<Kingdom.Building>();

            m_TilesUnderControl.Add(new Vector2(X, Y));
            Build("Town Hall");
        }

        public int CalculateFoodForNextPopulationUnit(int currentPopulation)
        {
            return Mathf.RoundToInt(8f + (2f * currentPopulation) * Mathf.Pow(1.25f, currentPopulation));
        }

        public int CalculateBuyableTileCount()
        {
            return m_TilesUnderControl.Count - m_Population;
        }

        public void PayUpkeepCosts()
        {
            // TODO: Pay buildings maintenance
            UnityEngine.Debug.Log("Paying upkeep costs for " + RealName);

            int foodForPeople;

            foodForPeople = Mathf.RoundToInt(m_Population * FOOD_CONSUMPTION_PER_POPULATION);

            if (m_FoodStored < foodForPeople)
            {
                UnityEngine.Debug.Log("Not enough food (" + m_FoodStored + ") to feed population (" + m_Population + ") requiring " + foodForPeople + " food units.");

                m_Population = m_Population - 1;
                m_FoodStored = CalculateFoodForNextPopulationUnit(m_Population) - (foodForPeople - m_FoodStored);

                OnPopulationDecrese(this);
            }
            else
            {
                m_FoodStored = m_FoodStored - foodForPeople;
            }
        }

        public void CollectResources()
        {
            UnityEngine.Debug.Log("Recollecting resources for city " + RealName);

            int food;
            int production;
            int research;
            int gold;
            int foodToGrow;

            food        =   m_PopulationInFood * FOOD_PER_POPULATION;
            production  =   m_PopulationInProduction * PRODUCTION_PER_POPULATION;
            research    =   m_PopulationInResearch * RESEARCH_PER_POPULATION;
            gold        =   0;
            foodToGrow  =   CalculateFoodForNextPopulationUnit(m_Population);

            foreach (Vector2 tilePosition in m_TilesUnderControl)
            {
                food        +=  GameController.Instance.Game.Resources.CalculateFoodGeneration(tilePosition);
                production  +=  GameController.Instance.Game.Resources.CalculateProductionGeneration(tilePosition);
                research    +=  GameController.Instance.Game.Resources.CalculateResearchGeneration(tilePosition);
                gold        +=  GameController.Instance.Game.Resources.CalculateGoldGeneration(tilePosition);
            }

            m_FoodStored += food;

            if (m_FoodStored >= foodToGrow)
            {
                m_Population = m_Population + 1;
                m_FoodStored = Mathf.RoundToInt(m_Population * FOOD_CONSUMPTION_PER_POPULATION);  // Enough to pay in the Upkeep and stay at 0

                OnPopulationGrow(this);
            }

            // TODO: Add production to the building queue

            UnityEngine.Debug.Log("Food collected: " + food);
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

            return root;
        }
	}
}
