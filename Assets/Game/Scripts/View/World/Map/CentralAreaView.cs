using MS.Model.World;
using UnityEngine;

namespace MS.Views.World.Map
{
    public class CentralAreaView : View<Model.World.Area>
    {
        public Transform TerrainHolder;

        public GameObject FertilePlainsPrefab;
        public GameObject FertileHillsPrefab;
        public GameObject FertileMountainPrefab;
        public GameObject FertileForestPrefab;
        public GameObject FertileWaterPrefab;

        public GameObject BarrenPlainsPrefab;
        public GameObject BarrenHillsPrefab;
        public GameObject BarrenMountainPrefab;
        public GameObject BarrenForestPrefab;
        public GameObject BarrenWaterPrefab;

        public GameObject DesertPlainsPrefab;
        public GameObject DesertHillsPrefab;
        public GameObject DesertMountainPrefab;
        public GameObject DesertForestPrefab;
        public GameObject DesertWaterPrefab;

        public GameObject FrozenPlainsPrefab;
        public GameObject FrozenHillsPrefab;
        public GameObject FrozenMountainPrefab;
        public GameObject FrozenForestPrefab;
        public GameObject FrozenWaterPrefab;

        public GameObject VolcanicPlainsPrefab;
        public GameObject VolcanicHillsPrefab;
        public GameObject VolcanicMountainPrefab;
        public GameObject VolcanicForestPrefab;
        public GameObject VolcanicWaterPrefab;

        protected GameObject m_InstantiatedPrefab;

        public override void UpdateView(Area element)
        {
            GameObject prefab;

            if (m_InstantiatedPrefab != null)
            {
                Destroy(m_InstantiatedPrefab);
            }

            prefab = SelectPrefab(element.TerrainType, element.TopographyType);

            m_InstantiatedPrefab = Utils.Instantiate(prefab, TerrainHolder, TerrainHolder.transform.position, TerrainHolder.transform.rotation);

            m_Model = element;
        }

        protected GameObject SelectPrefab(Area.ETerrainType terrain, Area.ETopographyType topography)
        {
            switch (terrain)
            {
                case Area.ETerrainType.Fertile:
                    switch (topography)
                    {
                        case Area.ETopographyType.Plains:
                            return FertilePlainsPrefab;

                        case Area.ETopographyType.Hills:
                            return FertileHillsPrefab;

                        case Area.ETopographyType.Mountain:
                            return FertileMountainPrefab;

                        case Area.ETopographyType.Forest:
                            return FertileForestPrefab;

                        case Area.ETopographyType.Water:
                            return FertileWaterPrefab;
                    }
                    break;

                case Area.ETerrainType.Barren:
                    switch (topography)
                    {
                        case Area.ETopographyType.Plains:
                            return BarrenPlainsPrefab;

                        case Area.ETopographyType.Hills:
                            return BarrenHillsPrefab;

                        case Area.ETopographyType.Mountain:
                            return BarrenMountainPrefab;

                        case Area.ETopographyType.Forest:
                            return BarrenForestPrefab;

                        case Area.ETopographyType.Water:
                            return BarrenWaterPrefab;
                    }
                    break;

                case Area.ETerrainType.Desert:
                    switch (topography)
                    {
                        case Area.ETopographyType.Plains:
                            return DesertPlainsPrefab;

                        case Area.ETopographyType.Hills:
                            return DesertHillsPrefab;

                        case Area.ETopographyType.Mountain:
                            return DesertMountainPrefab;

                        case Area.ETopographyType.Forest:
                            return DesertForestPrefab;

                        case Area.ETopographyType.Water:
                            return DesertWaterPrefab;
                    }
                    break;

                case Area.ETerrainType.Frozen:
                    switch (topography)
                    {
                        case Area.ETopographyType.Plains:
                            return FrozenPlainsPrefab;

                        case Area.ETopographyType.Hills:
                            return FrozenHillsPrefab;

                        case Area.ETopographyType.Mountain:
                            return FrozenMountainPrefab;

                        case Area.ETopographyType.Forest:
                            return FrozenForestPrefab;

                        case Area.ETopographyType.Water:
                            return FrozenWaterPrefab;
                    }
                    break;

                case Area.ETerrainType.Volcanic:
                    switch (topography)
                    {
                        case Area.ETopographyType.Plains:
                            return VolcanicPlainsPrefab;

                        case Area.ETopographyType.Hills:
                            return VolcanicHillsPrefab;

                        case Area.ETopographyType.Mountain:
                            return VolcanicMountainPrefab;

                        case Area.ETopographyType.Forest:
                            return VolcanicForestPrefab;

                        case Area.ETopographyType.Water:
                            return VolcanicWaterPrefab;
                    }
                    break;
            }

            return null;
        }
    }
}
