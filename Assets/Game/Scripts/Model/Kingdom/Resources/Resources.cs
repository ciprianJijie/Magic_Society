using System;
using SimpleJSON;

namespace MS.Model
{
    public class Resources : ModelElement
    {
        public Food Food;

        public Resources()
        {
            Food = new Food();
        }

        public int CalculateFoodProduction(int x, int y)
        {
            Tile        tile;
            MapElement  element;
            int         amount;

            tile        =   GameController.Instance.Game.Map.Grid.GetTile(x, y);
            element     =   GameController.Instance.Game.Map.Grid.GetElement(x, y);
            amount      =   0;

            switch (tile.TerrainType)
            {
                case Tile.Terrain.Fertile:
                    amount += 3;
                    break;
                case Tile.Terrain.Barren:
                case Tile.Terrain.Frozen:
                case Tile.Terrain.Volcanic:
                    amount += 1;
                    break;
                default:
                    break;
            }

            if (element != null)
            {
                // TODO: Check for elemnts that produce additional food
            }

            return amount;
        }

        public override void FromJSON(JSONNode json)
        {
            Food.Name = json["food"]["name"];
        }

        public override JSONNode ToJSON()
        {
            JSONClass root;
            JSONClass node;

            root = new JSONClass();

            node = new JSONClass();
            node.Add("name", Food.Name);
            root.Add("food", node);

            return root;
        }
    }
}

