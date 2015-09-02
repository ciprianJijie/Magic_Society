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

        protected GameObject GetPrefabByTerrain(Model.Tile.Terrain terrainType)
        {
            switch (terrainType)
            {
                case MS.Model.Tile.Terrain.Fertile:
                    return FertilePrefab;
                case MS.Model.Tile.Terrain.Barren:
                    return BarrenPrefab;
                case MS.Model.Tile.Terrain.Desert:
                    return DesertPrefab;
                case MS.Model.Tile.Terrain.Frozen:
                    return FrozenPrefab;
                case MS.Model.Tile.Terrain.Volcanic:
                    return VolcanicPrefab;
                default:
                    return null;
            }
        }

        protected Model.Tile.Terrain TerrainType
        {
            get
            {
                return GameController.Instance.Game.Map.Grid.GetTile(Model.X, Model.Y).TerrainType;
            }
        }
    }
}
