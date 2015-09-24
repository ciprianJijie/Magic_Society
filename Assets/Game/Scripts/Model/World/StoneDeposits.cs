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