using UnityEngine;
using SimpleJSON;

namespace MS.Model
{
    public class RecollectionPhase : Phase
    {
        public RecollectionPhase()
        {
            Name = "RECOLLECTION_PHASE";
        }

        public override void Execute()
        {
            // Search all cities
            Player.ClearCollectedCache();

            foreach (MapElement element in Managers.GameManager.Instance.Game.World.FindElements(Player))
            {
                City city;

                city = element as City;

                if (city != null)
                {
                    ResourceAdvancedAmount collected;
                    ResourceAdvancedAmount positive;
                    ResourceAdvancedAmount negative;

                    collected = city.Collect();
                    positive = new ResourceAdvancedAmount();
                    negative = new ResourceAdvancedAmount();

                    collected.Split(ref positive, ref negative);

                    city.Store(negative);
                    city.Store(positive);
                }

                if (city.CanGrowPopulation())
                {
                    city.GrowPopulation(1);
                    city.Food = 0;
                }
            }            

            if (Player is HumanPlayer)
            {
                MS.UI.PlayerGlobalResources.Instance.UpdateValues(Player.Gold, Player.GoldCollected, Player.Research, Player.ResearchCollected);
            }

            Finish();
        }
    }
}