using UnityEngine;
using SimpleJSON;
using System.Collections.Generic;
using System;

namespace MS.Model
{
	public class City : CollectableMapElement, IResourceWarehouse, IUpkeepMaintained
	{
        public static readonly int      FOOD_PER_WORKER                     =   2;
        public static readonly int      PRODUCTION_PER_WORKER               =   2;
        public static readonly int      RESEARCH_PER_WORKER                 =   2;
        public static readonly int      GOLD_PER_WORKER                     =   4;
        public static readonly float    FOOD_CONSUMPTION_PER_POPULATION     =   1.0f;

        public string RealName;

        // Recollection
        public int FoodWorkers;
        public int ProductionWorkers;
        public int ResearchWorkers;
        public int GoldWorkers;

        // Events
        public Events.CityEvent OnPopulationGrow    =   Events.DefaultAction;
        public Events.CityEvent OnPopulationDecrese =   Events.DefaultAction;

        // Elements of City
        protected int m_Population;
        protected int m_FoodStored;
        protected List<Vector2> m_TilesUnderControl;

        protected Kingdom.BuildingQueue m_BuildingQueue;

        protected List<MS.Model.Kingdom.Building> m_Buildings;
        protected Kingdom.Building m_DelayedBuilding;

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

        public IEnumerable<Kingdom.Building> Buildings
        {
            get
            {
                return m_Buildings;
            }
        }

        public Kingdom.BuildingQueue BuildingQueue
        {
            get
            {
                return m_BuildingQueue;
            }
        }

        public int AvailableWorkers
        {
            get
            {
                return Population - FoodWorkers - ProductionWorkers - GoldWorkers - ResearchWorkers;
            }
        }

        public City()
        {
            m_FoodStored            =   0;
            m_TilesUnderControl     =   new List<Vector2>();
            m_Buildings             =   new List<Kingdom.Building>();
            m_BuildingQueue         =   new Kingdom.BuildingQueue(this);
            m_FoodCollected         =   new ResourceAdvancedAmount();
            m_ProductionCollected   =   new ResourceAdvancedAmount();

            m_TilesUnderControl.Add(new Vector2(X, Y));

            GrowPopulation(1);

            Build("BUILDING_TOWNHALL");

            // Subscribe to events
            m_BuildingQueue.OnBuildingCompleted     +=  BuildDelayed;
        }

        public int CalculateFoodForNextPopulationUnit(int currentPopulation)
        {
            return Mathf.RoundToInt(20f + (4f * currentPopulation) * Mathf.Pow(1.25f, currentPopulation));
        }

        public int CalculateTurnsToProduce(int productionCost)
        {
            int production;

            production = CollectProduction().GetTotalAmount();

            return Mathf.CeilToInt((float)productionCost / (float)production);
        }

        public int CalculateFoodConsumption(int population)
        {
            return Mathf.FloorToInt(population * FOOD_CONSUMPTION_PER_POPULATION);
        }

        public int CalculateBuyableTileCount()
        {
            return m_TilesUnderControl.Count - m_Population;
        }

        public void PayUpkeepCosts()
        {
            //int foodForPeople;

            //foodForPeople = CalculateFoodConsumption(m_Population);

            //if (m_FoodStored < foodForPeople)
            //{
            //    m_FoodStored = CalculateFoodConsumption(m_Population - 1);

            //    DecreasePopulation(1);
            //}
            //else
            //{
            //    m_FoodStored = m_FoodStored - foodForPeople;
            //}
        }

        public bool CanGrowPopulation()
        {
            if (m_FoodStored >= CalculateFoodForNextPopulationUnit(m_Population))
            {
                return true;
            }
            return false;
        }

        public override ResourceAdvancedAmount Collect()
        {
            ResourceAdvancedAmount amount;

            amount = base.Collect();

            foreach (ResourceAmount singleAmount in CollectFood())
            {
                amount.AddAmount(singleAmount);
            }

            foreach (ResourceAmount singleAmount in CollectProduction())
            {
                amount.AddAmount(singleAmount);
            }

            foreach (ResourceAmount singleAmount in CollectGold())
            {
                amount.AddAmount(singleAmount);
            }

            foreach (ResourceAmount singleAmount in CollectResearch())
            {
                amount.AddAmount(singleAmount);
            }

            return amount;
        }

        public void Store(ResourceAmount amount)
        {
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

        public void Store(ResourceAdvancedAmount amount)
        {
            foreach (ResourceAmount singleAmount in amount)
            {
                Store(singleAmount);
            }
        }

        public void ClearCollectedCache()
        {
            m_FoodCollected.Clear();
            m_ProductionCollected.Clear();
        }

        public void Build(Kingdom.Building scheme)
        {
            Build(scheme.Name);
        }

        public void Build(string type)
        {
            Kingdom.Building building;

            building        =   Kingdom.Building.Factory.Create(type);
            building.Owner  =   Owner;
            building.City   =   this;

            m_Buildings.Add(building);
        }

        public void BuildDelayed(Kingdom.Building scheme)
        {
            BuildDelayed(scheme.Name);
        }

        public void BuildDelayed(string type)
        {
            Kingdom.Building building;

            building = Kingdom.Building.Factory.Create(type);
            building.Owner = Owner;
            building.City = this;

            m_DelayedBuilding = building;
        }

        public void BuildCompletedBuilding()
        {
            if (m_DelayedBuilding != null)
            {
                Build(m_DelayedBuilding);
                m_DelayedBuilding = null;
            }
        }

        public bool Has(Kingdom.Building building)
        {
            foreach (Kingdom.Building constructedBuilding in m_Buildings)
            {
                if (building.Name == constructedBuilding.Name)
                {
                    return true;
                }
            }
            return false;
        }

        public void GrowPopulation(int amount)
        {
            m_Population += amount;

            FoodWorkers += amount;

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
                if (FoodWorkers > 0)
                {
                    FoodWorkers--;
                }
                else if (ProductionWorkers > 0)
                {
                    ProductionWorkers--;
                }
                else if (ResearchWorkers > 0)
                {
                    ResearchWorkers--;
                }
                else if (GoldWorkers > 0)
                {
                    GoldWorkers--;
                }

                diff--;
            }

            OnPopulationDecrese(this);
        }

        /// <summary>
        /// Simulates the recollection of food, but does not store it in the Warehouse.
        /// </summary>
        /// <returns></returns>
        public ResourceAdvancedAmount CollectFood()
        {
            ResourceAdvancedAmount amount;

            amount = new ResourceAdvancedAmount();

            if (FoodWorkers > 0)
            {
                amount.AddAmount(new ResourceAmount(Game.Instance.Resources.Food, FoodWorkers * FOOD_PER_WORKER, this));
            }

            foreach (Kingdom.Building building in m_Buildings)
            {
                IResourceCollector collector;

                collector = building as IResourceCollector;

                if (collector != null)
                {
                    amount.AddAmount(new ResourceAmount(Game.Instance.Resources.Food, collector.CalculateEstimatedFood(), building));
                }
            }

            // Feed the citizens

            int foodForPopulation;

            foodForPopulation = CalculateFoodConsumption(m_Population);

            amount.AddAmount(new ResourceAmount(Game.Instance.Resources.Food, -foodForPopulation, this));

            return amount;
        }

        public ResourceAdvancedAmount CollectProduction()
        {
            ResourceAdvancedAmount amount;

            amount = new ResourceAdvancedAmount();

            if (ProductionWorkers > 0)
            {
                amount.AddAmount(new ResourceAmount(Game.Instance.Resources.Production, ProductionWorkers * PRODUCTION_PER_WORKER, this));
            }

            foreach (Kingdom.Building building in m_Buildings)
            {
                IResourceCollector collector;

                collector = building as IResourceCollector;

                if (collector != null)
                {
                    amount.AddAmount(new ResourceAmount(Game.Instance.Resources.Production, collector.CalculateEstimatedProduction(), building));
                }
            }

            return amount;
        }

        public ResourceAdvancedAmount CollectGold()
        {
            ResourceAdvancedAmount amount;

            amount = new ResourceAdvancedAmount();

            if (GoldWorkers > 0)
            {
                amount.AddAmount(new ResourceAmount(Game.Instance.Resources.Gold, GoldWorkers * GOLD_PER_WORKER, this));
            }

            foreach (Kingdom.Building building in m_Buildings)
            {
                IResourceCollector collector;

                collector = building as IResourceCollector;

                if (collector != null)
                {
                    amount.AddAmount(new ResourceAmount(Game.Instance.Resources.Gold, collector.CalculateEstimatedGold(), building));
                }
            }

            return amount;
        }

        public ResourceAdvancedAmount CollectResearch()
        {
            ResourceAdvancedAmount amount;

            amount = new ResourceAdvancedAmount();

            if (ResearchWorkers > 0)
            {
                amount.AddAmount(new ResourceAmount(Game.Instance.Resources.Research, ResearchWorkers * RESEARCH_PER_WORKER, this));
            }

            foreach (Kingdom.Building building in m_Buildings)
            {
                IResourceCollector collector;

                collector = building as IResourceCollector;

                if (collector != null)
                {
                    amount.AddAmount(new ResourceAmount(Game.Instance.Resources.Research, collector.CalculateEstimatedResearch(), building));
                }
            }

            return amount;
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
