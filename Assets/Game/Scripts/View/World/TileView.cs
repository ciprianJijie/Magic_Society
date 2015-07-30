using UnityEngine;

namespace MS
{
    public class TileView : View<Tile>
    {
        public GridController Owner;
        public Elevation FertileTilePrefab;
        public Elevation BarrenTilePrefab;
        public Elevation DesertTilePrefab;
        public Elevation FrozeTilePrefab;
        public Elevation VolcanicTilePrefab;

        private Elevation m_InstantiatedTile;

        public float CurrentVerticalOffset
        {
            get
            {
                return m_InstantiatedTile.VerticalOffset * m_InstantiatedTile.Height;
            }
        }

        public override void UpdateView()
        {
            if (m_InstantiatedTile != null)
            {
                Destroy(m_InstantiatedTile.gameObject);
            }

            Elevation tileToInstantiate;

            switch (m_Model.TerrainType)
            {
                case Tile.Terrain.Barren:
                    tileToInstantiate = BarrenTilePrefab;
                    break;
                case Tile.Terrain.Desert:
                    tileToInstantiate = DesertTilePrefab;
                    break;
                case Tile.Terrain.Frozen:
                    tileToInstantiate = FrozeTilePrefab;
                    break;
                case Tile.Terrain.Volcanic:
                    tileToInstantiate = VolcanicTilePrefab;
                    break;
                default:
                    tileToInstantiate = FertileTilePrefab;
                    break;
            }
            m_InstantiatedTile = Utils.Instantiate<Elevation>(tileToInstantiate, this.transform, this.transform.position, this.transform.rotation);

            m_InstantiatedTile.ChangeHeight(m_Model.Height);

            int heightToEnsure;

            heightToEnsure = Owner.Grid.GetLowestNeighborHeight(m_Model.X, m_Model.Y);

            if (m_Model.Height > heightToEnsure)
            {
                m_InstantiatedTile.FillFromCurrentTo(heightToEnsure);
            }
        }
    }
}
