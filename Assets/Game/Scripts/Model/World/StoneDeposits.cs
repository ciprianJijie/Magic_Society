using System.Collections.Generic;

namespace MS.Model
{
    public class StoneDeposits : MapElement, IResourceCollector
    {
        public static readonly int STONE_DEPOSITS_FOOD = 0;
        public static readonly int STONE_DEPOSITS_PRODUCTION = 4;
        public static readonly int STONE_DEPOSITS_GOLD = 0;
        public static readonly int STONE_DEPOSITS_RESEARCH = 0;

        public StoneDeposits()
            : base (0, 0, "MAP_ELEMENT_STONE_DEPOSITS")
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
            return STONE_DEPOSITS_FOOD;
        }

        public int CalculateEstimatedProduction()
        {
            return STONE_DEPOSITS_PRODUCTION;
        }

        public int CalculateEstimatedGold()
        {
            return STONE_DEPOSITS_GOLD;
        }

        public int CalculateEstimatedResearch()
        {
            return STONE_DEPOSITS_RESEARCH;
        }
    }
}