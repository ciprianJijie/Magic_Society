using System;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

namespace MS.Model
{
    public class CollectableMapElement : OwnableMapElement, IResourceCollector
    {
        public int FoodGeneration;
        public int ProductionGeneration;
        public int GoldGeneration;
        public int ResearchGeneration;

        protected Player  m_Owner;

        public virtual IEnumerable<ResourceAmount> Collect()
        {
            if (FoodGeneration > 0)
            {
                yield return new ResourceAmount(Game.Instance.Resources.Food, FoodGeneration, this);
            }

            if (ProductionGeneration > 0)
            {
                yield return new ResourceAmount(Game.Instance.Resources.Production, ProductionGeneration, this);
            }

            if (GoldGeneration > 0)
            {
                yield return new ResourceAmount(Game.Instance.Resources.Gold, ProductionGeneration, this);
            }

            if (ResearchGeneration > 0)
            {
                yield return new ResourceAmount(Game.Instance.Resources.Research, ResearchGeneration, this);
            }
        }

        public override void FromJSON(JSONNode json)
        {
            base.FromJSON(json);

            FoodGeneration = json["resources_generation"]["food"].AsInt;
            ProductionGeneration = json["resources_generation"]["production"].AsInt;
            GoldGeneration = json["resources_generation"]["gold"].AsInt;
            ResearchGeneration = json["resources_generation"]["research"].AsInt;
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
