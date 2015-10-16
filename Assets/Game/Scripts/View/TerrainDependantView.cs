using UnityEngine;

namespace MS.Views
{
    public abstract class TerrainDependantView<T> : View<T> where T : Model.MapElement
    {
        public GameObject FertilePrefab;
        public GameObject BarrenPrefab;
        public GameObject DesertPrefab;
        public GameObject FrozenPrefab;
        public GameObject VolcanicPrefab;

        protected GameObject GetPrefabByTerrain(Model.World.Area.ETerrainType terrainType)
        {
            switch (terrainType)
            {
                case MS.Model.World.Area.ETerrainType.Fertile:
                    return FertilePrefab;
                case MS.Model.World.Area.ETerrainType.Barren:
                    return BarrenPrefab;
                case MS.Model.World.Area.ETerrainType.Desert:
                    return DesertPrefab;
                case MS.Model.World.Area.ETerrainType.Frozen:
                    return FrozenPrefab;
                case MS.Model.World.Area.ETerrainType.Volcanic:
                    return VolcanicPrefab;
                default:
                    return null;
            }
        }

        protected Model.World.Area.ETerrainType TerrainType
        {
            get
            {
                return Managers.GameManager.Instance.Game.World.GetRegion(Model.X, Model.Y).CapitalArea.TerrainType;
            }
        }
    }
}
