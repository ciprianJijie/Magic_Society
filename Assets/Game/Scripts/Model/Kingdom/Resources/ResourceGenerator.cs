using System;
using System.Collections.Generic;
using SimpleJSON;

namespace MS.Model
{
    public class ResourceGenerator : ModelElement, IResourceCollector
    {
        public int FoodGeneration;
        public int ProductionGeneration;
        public int GoldGeneration;
        public int ResearchGeneration;

        public ResourceGenerator(int food, int production, int gold, int research)
        {
            FoodGeneration        =   food;
            ProductionGeneration  =   production;
            GoldGeneration        =   gold;
            ResearchGeneration    =   research;
        }

        public virtual ResourceAdvancedAmount Collect()
        {
            ResourceAdvancedAmount amount;
            int food;
            int production;
            int gold;
            int research;

            amount = new ResourceAdvancedAmount();
            food = CalculateEstimatedFood();
            production = CalculateEstimatedProduction();
            gold = CalculateEstimatedGold();
            research = CalculateEstimatedResearch();

            if (food > 0)
            {
                amount.AddAmount(new ResourceAmount(Game.Instance.Resources.Food, food, this));
            }

            if (production > 0)
            {
                amount.AddAmount(new ResourceAmount(Game.Instance.Resources.Production, production, this));
            }

            if (gold > 0)
            {
                amount.AddAmount(new ResourceAmount(Game.Instance.Resources.Gold, gold, this));
            }

            if (research > 0)
            {
                amount.AddAmount(new ResourceAmount(Game.Instance.Resources.Research, research, this));
            }

            return amount;
        }

        public override void FromJSON(JSONNode json)
        {
            base.FromJSON(json);

            FoodGeneration          =   json["resources_generation"]["food"].AsInt;
            ProductionGeneration    =   json["resources_generation"]["production"].AsInt;
            GoldGeneration          =   json["resources_generation"]["gold"].AsInt;
            ResearchGeneration      =   json["resources_generation"]["research"].AsInt;
        }

        public override JSONNode ToJSON()
        {
            JSONNode root;
            JSONClass resources;

            root = base.ToJSON();
            resources = new JSONClass();
            resources.Add("food", new JSONData(FoodGeneration));
            resources.Add("production", new JSONData(ProductionGeneration));
            resources.Add("gold", new JSONData(GoldGeneration));
            resources.Add("research", new JSONData(ResearchGeneration));
            root.Add("resources_generation", resources);

            return root;
        }

        public int CalculateEstimatedFood()
        {
            return FoodGeneration;
        }

        public int CalculateEstimatedProduction()
        {
            return ProductionGeneration;
        }

        public int CalculateEstimatedGold()
        {
            return GoldGeneration;
        }

        public int CalculateEstimatedResearch()
        {
            return ResearchGeneration;
        }
    }
}
