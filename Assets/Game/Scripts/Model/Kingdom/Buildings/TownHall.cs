using System;
using System.Collections.Generic;
using UnityEngine;

namespace MS.Model.Kingdom
{
    public class TownHall : Building, IResourceCollector
    {
        public static int TOWNHALL_FOOD_COLLECTION          =   4;
        public static int TOWNHALL_PRODUCTION_COLLECTION    =   2;
        public static int TOWNHALL_GOLD_COLLECTION          =   6;
        public static int TOWNHALL_RESEARCH_COLLECTION      =   2;

        public TownHall()
        {
            Name = "BUILDING_TOWNHALL";
            Description = "BUILDING_TOWNHALL_DESCRIPTION";
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
            return TOWNHALL_FOOD_COLLECTION;
        }

        public int CalculateEstimatedProduction()
        {
            return TOWNHALL_PRODUCTION_COLLECTION;
        }

        public int CalculateEstimatedGold()
        {
            return TOWNHALL_GOLD_COLLECTION;
        }

        public int CalculateEstimatedResearch()
        {
            return TOWNHALL_RESEARCH_COLLECTION;
        }
    }
}

