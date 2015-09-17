using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

namespace MS.Model
{
    public class Tile : ModelElement, IResourceCollector
	{
        public static readonly int TILE_FERTILE_FOOD = 3;
        public static readonly int TILE_FERTILE_PRODUCTION = 0;
        public static readonly int TILE_FERTILE_GOLD = 0;
        public static readonly int TILE_FERTILE_RESEARCH = 0;
        public static readonly int TILE_BARREN_FOOD = 1;
        public static readonly int TILE_BARREN_PRODUCTION = 1;
        public static readonly int TILE_BARREN_GOLD = 1;
        public static readonly int TILE_BARREN_RESEARCH = 0;
        public static readonly int TILE_DESERT_FOOD = 0;
        public static readonly int TILE_DESERT_PRODUCTION = 1;
        public static readonly int TILE_DESERT_GOLD = 3;
        public static readonly int TILE_DESERT_RESEARCH = 0;
        public static readonly int TILE_FROZEN_FOOD = 1;
        public static readonly int TILE_FROZEN_PRODUCTION = 0;
        public static readonly int TILE_FROZEN_GOLD = 1;
        public static readonly int TILE_FROZEN_RESEARCH = 2;
        public static readonly int TILE_VOLCANIC_FOOD = 0;
        public static readonly int TILE_VOLCANIC_PRODUCTION = 3;
        public static readonly int TILE_VOLCANIC_GOLD = 0;
        public static readonly int TILE_VOLCANIC_RESEARCH = 1;

		// Terrain Type
		public enum Terrain { Fertile, Barren, Desert, Frozen, Volcanic }
		// ---

        public int          X;
        public int          Y;

        public int 			Height;
        public Terrain 		TerrainType;

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
            switch (TerrainType)
            {
                case Terrain.Barren:
                    return TILE_BARREN_FOOD;
                case Terrain.Desert:
                    return TILE_DESERT_FOOD;
                case Terrain.Fertile:
                    return TILE_FERTILE_FOOD;
                case Terrain.Frozen:
                    return TILE_FROZEN_FOOD;
                case Terrain.Volcanic:
                    return TILE_VOLCANIC_FOOD;
                default:
                    return 0;
            }
        }

        public int CalculateEstimatedProduction()
        {
            switch (TerrainType)
            {
                case Terrain.Barren:
                    return TILE_BARREN_PRODUCTION;
                case Terrain.Desert:
                    return TILE_DESERT_PRODUCTION;
                case Terrain.Fertile:
                    return TILE_FERTILE_PRODUCTION;
                case Terrain.Frozen:
                    return TILE_FROZEN_PRODUCTION;
                case Terrain.Volcanic:
                    return TILE_VOLCANIC_PRODUCTION;
                default:
                    return 0;
            }
        }

        public int CalculateEstimatedGold()
        {
            switch (TerrainType)
            {
                case Terrain.Barren:
                    return TILE_BARREN_GOLD;
                case Terrain.Desert:
                    return TILE_DESERT_GOLD;
                case Terrain.Fertile:
                    return TILE_FERTILE_GOLD;
                case Terrain.Frozen:
                    return TILE_FROZEN_GOLD;
                case Terrain.Volcanic:
                    return TILE_VOLCANIC_GOLD;
                default:
                    return 0;
            }
        }

        public int CalculateEstimatedResearch()
        {
            switch (TerrainType)
            {
                case Terrain.Barren:
                    return TILE_BARREN_RESEARCH;
                case Terrain.Desert:
                    return TILE_DESERT_RESEARCH;
                case Terrain.Fertile:
                    return TILE_FERTILE_RESEARCH;
                case Terrain.Frozen:
                    return TILE_FROZEN_RESEARCH;
                case Terrain.Volcanic:
                    return TILE_FROZEN_RESEARCH;
                default:
                    return 0;
            }
        }

        public override void FromJSON(JSONNode json)
		{
            X               =   json["x"].AsInt;
            Y               =   json["y"].AsInt;
            Height 			= 	json["height"].AsInt;
            TerrainType 	= 	EnumUtils.ParseEnum<Terrain>(json["terrain"]);

            switch (TerrainType)
            {
                case Terrain.Barren:
                    Name = "TILE_BARREN";
                    break;
                case Terrain.Desert:
                    Name = "TILE_DESERT";
                    break;
                case Terrain.Fertile:
                    Name = "TILE_FERTILE";
                    break;
                case Terrain.Frozen:
                    Name = "TILE_FROZEN";
                    break;
                case Terrain.Volcanic:
                    Name = "TILE_VOLCANIC";
                    break;
            }
        }

		public override JSONNode ToJSON()
		{
            JSONClass root = new JSONClass();

            root.Add("x", new JSONData(X));
            root.Add("y", new JSONData(Y));
            root.Add("height", new JSONData(Height));
            root.Add("terrain", TerrainType.ToString());

            return root;
        }
    }
}
