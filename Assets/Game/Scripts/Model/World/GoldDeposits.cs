using System.Collections.Generic;

namespace MS.Model
{
	public class GoldDeposits : MapElement, IResourceCollector
	{
        public static readonly int GOLD_DEPOSITS_FOOD = 0;
        public static readonly int GOLD_DEPOSITS_PRODUCTION = 0;
        public static readonly int GOLD_DEPOSITS_GOLD = 10;
        public static readonly int GOLD_DEPOSITS_RESEARCH = 0;

        public GoldDeposits()
			: base(0, 0, "MAP_ELEMENT_GOLD_DEPOSITS")
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
            return GOLD_DEPOSITS_FOOD;
        }

        public int CalculateEstimatedProduction()
        {
            return GOLD_DEPOSITS_PRODUCTION;
        }

        public int CalculateEstimatedGold()
        {
            return GOLD_DEPOSITS_GOLD;
        }

        public int CalculateEstimatedResearch()
        {
            return GOLD_DEPOSITS_RESEARCH;
        }
	}
}
