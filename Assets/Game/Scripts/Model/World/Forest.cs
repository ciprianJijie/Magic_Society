using UnityEngine;
using System.Collections.Generic;

namespace MS.Model
{
    public class Forest : MapElement, IResourceCollector
    {
        public static readonly int FOREST_FOOD = 0;
        public static readonly int FOREST_PRODUCTION = 3;
        public static readonly int FOREST_GOLD = 0;
        public static readonly int FOREST_RESEARCH = 0;

        public Forest()
            : base(0, 0, "MAP_ELEMENT_FOREST")
        {
            
        }

        public ResourceAdvancedAmount Collect()
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

        public int CalculateEstimatedFood()
        {
            return FOREST_FOOD;
        }

        public int CalculateEstimatedProduction()
        {
            return FOREST_PRODUCTION;
        }

        public int CalculateEstimatedGold()
        {
            return FOREST_GOLD;
        }

        public int CalculateEstimatedResearch()
        {
            return FOREST_RESEARCH;
        }
    }
}
