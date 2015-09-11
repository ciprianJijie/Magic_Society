using UnityEngine;
using SimpleJSON;
using System.Collections.Generic;
using System;

namespace MS.Model
{
	public class City : CollectableMapElement, IResourceWarehouse, IUpkeepMaintained
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

        // Resources
        protected ResourceAdvancedAmount m_FoodCollected;
        protected ResourceAdvancedAmount m_ProductionCollected;

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
            m_FoodCollected        =   new ResourceAdvancedAmount();
            m_ProductionCollected  =   new ResourceAdvancedAmount();

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
            UnityEngine.Debug.Log("Paying upkeep for " + RealName);
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

        public override IEnumerable<ResourceAmount> Collect()
        {
            UnityEngine.Debug.Log("Collecting resources of " + RealName);

            List<ResourceAmount> collected = new List<ResourceAmount>();
            ResourceAmount food;
            ResourceAmount production;
            ResourceAmount gold;
            ResourceAmount research;

            ClearCollectedCache();

            foreach (var res in base.Collect())
            {
                collected.Add(res);
                Store(res);
            }

            food        =   new ResourceAmount(Game.Instance.Resources.Food, m_PopulationInFood * FOOD_PER_POPULATION, this);
            production  =   new ResourceAmount(Game.Instance.Resources.Production, m_PopulationInProduction * PRODUCTION_PER_POPULATION, this);
            gold        =   new ResourceAmount(Game.Instance.Resources.Gold, m_PopulationInGold * GOLD_PER_POPULATION, this);
            research    =   new ResourceAmount(Game.Instance.Resources.Research, m_PopulationInResearch * RESEARCH_PER_POPULATION, this);

            collected.Add(food);
            collected.Add(production);
            collected.Add(gold);
            collected.Add(research);

            Store(food);
            Store(production);
            Store(gold);
            Store(research);

            // Buildings
            foreach (Model.Kingdom.Building building in m_Buildings)
            {
                IResourceCollector collector;

                collector = building as IResourceCollector;

                if (collector != null)
                {
                    foreach (var res in collector.Collect())
                    {
                        collected.Add(res);
                        Store(res);
                    }
                }
            }

            // Check for population growth
            int foodToGrow;

            foodToGrow = CalculateFoodForNextPopulationUnit(m_Population);

            if (m_FoodStored >= foodToGrow)
            {
                m_FoodStored = Mathf.RoundToInt((m_Population + 1) * FOOD_CONSUMPTION_PER_POPULATION);  // Enough to pay in the Upkeep and stay at 0
                GrowPopulation(1);
            }

            return collected;
        }

        public void Store(ResourceAmount amount)
        {
            //UnityEngine.Debug.Log(RealName + " storing " + amount.ToString());

            if (amount.Resource is Food)
            {
                m_FoodCollected.AddAmount(amount);
                m_FoodStored += amount.Amount;
            }
            else if (amount.Resource is Production)
            {
                m_ProductionCollected.AddAmount(amount);
                m_BuildingQueue.AddProduction(amount.Amount);
            }
            else if (amount.Resource is Gold)
            {
                Owner.Store(amount);
            }
            else if (amount.Resource is Research)
            {
                Owner.Store(amount);
            }
        }

        public void ClearCollectedCache()
        {
            m_FoodCollected.Clear();
            m_ProductionCollected.Clear();
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
