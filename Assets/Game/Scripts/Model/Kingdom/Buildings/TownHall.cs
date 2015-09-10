using System;
using UnityEngine;

namespace MS.Model.Kingdom
{
    public class TownHall : Building
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
            // TOD: Add resources to the player owning this building
            City.Food += TOWNHALL_FOOD_COLLECTION;
            // City.Production += TOWNHALL_PRODUCTION_COLLECTION;
            Owner.Gold += TOWNHALL_GOLD_COLLECTION;
            Owner.Research += TOWNHALL_RESEARCH_COLLECTION;
        }

        public override void OnUpkeep()
        {
            // Townhall has no Upkeep value
        }

        public override void Use()
        {
            // Townhall has no activated ability
        }
    }
}

