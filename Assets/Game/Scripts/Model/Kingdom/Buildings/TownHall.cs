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
        }
        
        public override void OnRecollection()
        {
            //City.Food       +=  TOWNHALL_FOOD_COLLECTION;
            //Owner.Gold      +=  TOWNHALL_GOLD_COLLECTION;
            //Owner.Research  +=  TOWNHALL_RESEARCH_COLLECTION;

            //City.BuildingQueue.AddProduction(TOWNHALL_PRODUCTION_COLLECTION);
        }

        public override void OnUpkeep()
        {
            // Townhall has no Upkeep value
        }

        public override void Use()
        {
            // Townhall has no activated ability
        }

        public IEnumerable<ResourceAmount> Collect()
        {
            yield return new ResourceAmount(Game.Instance.Resources.Food, TOWNHALL_FOOD_COLLECTION, this);
            yield return new ResourceAmount(Game.Instance.Resources.Production, TOWNHALL_PRODUCTION_COLLECTION, this);
            yield return new ResourceAmount(Game.Instance.Resources.Gold, TOWNHALL_GOLD_COLLECTION, this);
            yield return new ResourceAmount(Game.Instance.Resources.Research, TOWNHALL_RESEARCH_COLLECTION, this);
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

