﻿using UnityEngine;
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

            foreach (MapElement element in GameController.Instance.Game.Map.Grid.GetElements(Player))
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
            }            

            if (Player is HumanPlayer)
            {
                MS.UI.PlayerGlobalResources.Instance.UpdateValues(Player.Gold, Player.GoldCollected, Player.Research, Player.ResearchCollected);
            }

            Finish();
        }
    }
}