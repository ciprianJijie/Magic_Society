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

        public IEnumerable<ResourceAmount> Collect()
        {
            int food;
            int production;
            int gold;
            int research;

            food = CalculateEstimatedFood();
            production = CalculateEstimatedProduction();
            gold = CalculateEstimatedGold();
            research = CalculateEstimatedResearch();

            if (food > 0)
            {
                yield return new ResourceAmount(Game.Instance.Resources.Food, food, this);
            }

            if (production > 0)
            {
                yield return new ResourceAmount(Game.Instance.Resources.Production, production, this);
            }

            if (gold > 0)
            {
                yield return new ResourceAmount(Game.Instance.Resources.Gold, gold, this);
            }

            if (research > 0)
            {
                yield return new ResourceAmount(Game.Instance.Resources.Research, research, this);
            }
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
