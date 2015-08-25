using System;
using SimpleJSON;

namespace MS.Model
{
    public class Resources : ModelElement
    {
        public Food Food;
        public Wood Wood;
        public Iron Iron;
        public Gold Gold;
        public Mana Mana;

        public Resources()
        {
            Food = new Food();
            Wood = new Wood();
            Iron = new Iron();
            Gold = new Gold();
            Mana = new Mana();
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

        public int CalculateWoodProduction(int x, int y)
        {
            Tile        tile;
            MapElement  element;
            int         amount;

            tile        =   GameController.Instance.Game.Map.Grid.GetTile(x, y);
            element     =   GameController.Instance.Game.Map.Grid.GetElement(x, y);
            amount      =   0;

            if (element != null)
            {
                if (element is Forest)
                {
                    switch (tile.TerrainType)
                    {
                        case Tile.Terrain.Barren:
                        case Tile.Terrain.Desert:
                        case Tile.Terrain.Volcanic:
                            amount += 1;
                            break;
                        case Tile.Terrain.Fertile:
                            amount += 2;
                            break;
                        case Tile.Terrain.Frozen:
                            amount += 3;
                            break;
                        default:
                            break;
                    }
                }
            }

            return amount;
        }

        public int CalculateIronProduction(int x, int y)
        {
            MapElement  element;
            int         amount;

            element     =   GameController.Instance.Game.Map.Grid.GetElement(x, y);
            amount      =   0;

            if (element != null)
            {
                if (element is StoneDeposits)
                {
                    amount += 3;
                }
            }

            return amount;
        }

        public int CalculateGoldProduction(int x, int y)
        {
            Tile        tile;
            MapElement  element;
            int         amount;

            tile        =   GameController.Instance.Game.Map.Grid.GetTile(x, y);
            element     =   GameController.Instance.Game.Map.Grid.GetElement(x, y);
            amount      =   0;

            switch (tile.TerrainType)
            {
                case Tile.Terrain.Barren:
                    amount += 1;
                    break;
                case Tile.Terrain.Desert:
                case Tile.Terrain.Fertile:
                case Tile.Terrain.Volcanic:
                default:
                    break;
            }

            if (element != null && element is GoldDeposits)
            {
                amount += 3;
            }

            return amount;
        }

        public int CalculateManaProduction(int x, int y)
        {
            Tile        tile;
            MapElement  element;
            int         amount;

            tile        =   GameController.Instance.Game.Map.Grid.GetTile(x, y);
            element     =   GameController.Instance.Game.Map.Grid.GetElement(x, y);
            amount      =   0;

            switch (tile.TerrainType)
            {
                case Tile.Terrain.Frozen:
                    amount += 2;
                    break;
                case Tile.Terrain.Desert:
                    amount += 1;
                    break;
                case Tile.Terrain.Barren:
                case Tile.Terrain.Fertile:
                case Tile.Terrain.Volcanic:
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
            Food.Name = json["food"]["name"];
            Wood.Name = json["wood"]["name"];
            Iron.Name = json["iron"]["name"];
            Gold.Name = json["gold"]["name"];
            Mana.Name = json["mana"]["name"];
        }

        public override JSONNode ToJSON()
        {
            JSONClass root;
            JSONClass node;

            root = new JSONClass();

            node = new JSONClass();
            node.Add("name", Food.Name);
            root.Add("food", node);

            node = new JSONClass();
            node.Add("name", Wood.Name);
            root.Add("wood", node);

            node = new JSONClass();
            node.Add("name", Iron.Name);
            root.Add("iron", node);

            node = new JSONClass();
            node.Add("name", Gold.Name);
            root.Add("gold", node);

            node = new JSONClass();
            node.Add("name", Mana.Name);
            root.Add("mana", node);

            return root;
        }
    }
}

