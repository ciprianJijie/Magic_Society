using System;
using SimpleJSON;

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

        public int CalculateFoodGeneration(int x, int y)
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
                    amount += 1;
                    break;
                case Tile.Terrain.Frozen:
                    amount += 1;
                    break;
                case Tile.Terrain.Volcanic:
                    amount += 1;
                    break;
                case Tile.Terrain.Desert:
                    break;
                default:
                    break;
            }

            if (element != null)
            {

            }

            return amount;
        }

        public int CalculateProductionGeneration(int x, int y)
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
                    break;
                case Tile.Terrain.Barren:
                    break;
                case Tile.Terrain.Desert:
                    break;
                case Tile.Terrain.Frozen:
                    break;
                case Tile.Terrain.Volcanic:
                    amount += 2;
                    break;
                default:
                    break;
            }

            if (element != null)
            {
                if (element is Forest)
                {
                    amount += 3;
                }
                else if (element is StoneDeposits)
                {
                    amount += 5;
                }
            }

            return amount;
        }

        public int CalculateGoldGeneration(int x, int y)
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
                    break;
                case Tile.Terrain.Barren:
                    amount += 2;
                    break;
                case Tile.Terrain.Desert:
                    amount += 4;
                    break;
                case Tile.Terrain.Frozen:
                    break;
                case Tile.Terrain.Volcanic:
                    break;
                default:
                    break;
            }

            if (element != null)
            {
                if (element is GoldDeposits)
                {
                    amount += 5;
                }
            }

            return amount;
        }

        public int CalculateResearchGeneration(int x, int y)
        {
            Tile tile;
            MapElement element;
            int amount;

            tile = GameController.Instance.Game.Map.Grid.GetTile(x, y);
            element = GameController.Instance.Game.Map.Grid.GetElement(x, y);
            amount = 0;

            switch (tile.TerrainType)
            {
                case Tile.Terrain.Fertile:
                    break;
                case Tile.Terrain.Barren:
                    break;
                case Tile.Terrain.Desert:
                    break;
                case Tile.Terrain.Frozen:
                    amount += 2;
                    break;
                case Tile.Terrain.Volcanic:
                    amount += 1;
                    break;
                default:
                    break;
            }

            if (element != null)
            {

            }

            return amount;
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

