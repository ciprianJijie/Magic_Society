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
