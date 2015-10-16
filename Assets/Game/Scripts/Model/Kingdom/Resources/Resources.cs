using System;
using SimpleJSON;
using UnityEngine;

namespace MS.Model
{
    public class Resources : ModelElement
    {
        public Food         Food;
        public Production   Production;
        public Gold         Gold;
        public Research     Research;

        public Resources()
        {
            Food        =   new Food();
            Production  =   new Production();
            Gold        =   new Gold();
            Research    =   new Research();
        }

        public int CalculateFoodGeneration(Vector2 position)
        {
            return CalculateFoodGeneration((int)position.x, (int)position.y);
        }

        public int CalculateFoodGeneration(int x, int y)
        {
            throw new System.NotImplementedException();
        }

        public int CalculateProductionGeneration(Vector2 tilePosition)
        {
            return CalculateProductionGeneration((int)tilePosition.x, (int)tilePosition.y);
        }

        public int CalculateProductionGeneration(int x, int y)
        {
            throw new System.NotImplementedException();
        }

        public int CalculateGoldGeneration(Vector2 tilePosition)
        {
            return CalculateGoldGeneration((int)tilePosition.x, (int)tilePosition.y);
        }

        public int CalculateGoldGeneration(int x, int y)
        {
            throw new System.NotImplementedException();
        }

        public int CalculateResearchGeneration(Vector2 tilePosition)
        {
            return CalculateResearchGeneration((int)tilePosition.x, (int)tilePosition.y);
        }

        public int CalculateResearchGeneration(int x, int y)
        {
            throw new System.NotImplementedException();
        }


        public override void FromJSON(JSONNode json)
        {
            Food.FromJSON(json["food"]);
            Production.FromJSON(json["production"]);
            Gold.FromJSON(json["gold"]);
            Research.FromJSON(json["research"]);
        }

        public override JSONNode ToJSON()
        {
            JSONClass root;

            root = new JSONClass();

            root.Add("food", Food.ToJSON());
            root.Add("production", Production.ToJSON());
            root.Add("gold", Production.ToJSON());
            root.Add("research", Research.ToJSON());

            return root;
        }
    }
}

