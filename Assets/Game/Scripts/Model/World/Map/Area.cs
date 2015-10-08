
using SimpleJSON;

namespace MS.Model.World
{
    public class Area : ModelElement, IResourceCollector
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

        public enum ETerrainType
        {
            Fertile,
            Barren,
            Desert,
            Frozen,
            Volcanic
        }

        public enum ETopographyType
        {
            Plains,
            Hills,
            Mountain,
            Forest,
            Water
        }

        public ETerrainType     TerrainType;
        public ETopographyType  TopographyType;
        public bool             CanBuildVillage;

        public ResourceAdvancedAmount Collect()
        {
            ResourceAmount          food;
            ResourceAmount          production;
            ResourceAmount          gold;
            ResourceAmount          research;
            ResourceAdvancedAmount  collected;

            food        =   new ResourceAmount(Game.Instance.Resources.Food, CalculateEstimatedFood(), this);
            production  =   new ResourceAmount(Game.Instance.Resources.Production, CalculateEstimatedProduction(), this);
            gold        =   new ResourceAmount(Game.Instance.Resources.Gold, CalculateEstimatedGold(), this);
            research    =   new ResourceAmount(Game.Instance.Resources.Research, CalculateEstimatedResearch(), this);
            collected   =   new ResourceAdvancedAmount();

            collected.AddAmount(food);
            collected.AddAmount(production);
            collected.AddAmount(gold);
            collected.AddAmount(research);


            return collected;
        }

        public int CalculateEstimatedFood()
        {
            int amount;

            amount = 0;

            switch (TerrainType)
            {
                case ETerrainType.Barren:
                    amount += TILE_BARREN_FOOD;
                    break;
                case ETerrainType.Desert:
                    amount += TILE_DESERT_FOOD;
                    break;
                case ETerrainType.Fertile:
                    amount += TILE_FERTILE_FOOD;
                    break;
                case ETerrainType.Frozen:
                    amount += TILE_FROZEN_FOOD;
                    break;
                case ETerrainType.Volcanic:
                    amount += TILE_VOLCANIC_FOOD;
                    break;
                default:
                    break;
            }

            return amount;
        }

        public int CalculateEstimatedProduction()
        {
            int amount;

            amount = 0;

            switch (TerrainType)
            {
                case ETerrainType.Barren:
                    amount += TILE_BARREN_PRODUCTION;
                    break;
                case ETerrainType.Desert:
                    amount += TILE_DESERT_PRODUCTION;
                    break;
                case ETerrainType.Fertile:
                    amount += TILE_FERTILE_PRODUCTION;
                    break;
                case ETerrainType.Frozen:
                    amount += TILE_FROZEN_PRODUCTION;
                    break;
                case ETerrainType.Volcanic:
                    amount += TILE_VOLCANIC_PRODUCTION;
                    break;
                default:
                    break;
            }

            return amount;
        }

        public int CalculateEstimatedGold()
        {
            int amount;

            amount = 0;

            switch (TerrainType)
            {
                case ETerrainType.Barren:
                    amount += TILE_BARREN_GOLD;
                    break;
                case ETerrainType.Desert:
                    amount += TILE_DESERT_GOLD;
                    break;
                case ETerrainType.Fertile:
                    amount += TILE_FERTILE_GOLD;
                    break;
                case ETerrainType.Frozen:
                    amount += TILE_FROZEN_GOLD;
                    break;
                case ETerrainType.Volcanic:
                    amount += TILE_VOLCANIC_GOLD;
                    break;
                default:
                    break;
            }

            return amount;
        }

        public int CalculateEstimatedResearch()
        {
            int amount;

            amount = 0;

            switch (TerrainType)
            {
                case ETerrainType.Barren:
                    amount += TILE_BARREN_RESEARCH;
                    break;
                case ETerrainType.Desert:
                    amount += TILE_DESERT_RESEARCH;
                    break;
                case ETerrainType.Fertile:
                    amount += TILE_FERTILE_RESEARCH;
                    break;
                case ETerrainType.Frozen:
                    amount += TILE_FROZEN_RESEARCH;
                    break;
                case ETerrainType.Volcanic:
                    amount += TILE_VOLCANIC_RESEARCH;
                    break;
                default:
                    break;
            }

            return amount;
        }

        public override void FromJSON(JSONNode json)
        {
            base.FromJSON(json);

            TerrainType     =   EnumUtils.ParseEnum<ETerrainType>(json["terrain"]);
            TopographyType  =   EnumUtils.ParseEnum<ETopographyType>(json["topography"]);
            CanBuildVillage =   json["village_slot"].AsBool;
        }

        public override JSONNode ToJSON()
        {
            JSONNode root;
            
            root = base.ToJSON();

            root.Add("terrain", TerrainType.ToString());
            root.Add("topography", TopographyType.ToString());
            root.Add("village_slot", new JSONData(CanBuildVillage));

            return root;
        }
    }
}
