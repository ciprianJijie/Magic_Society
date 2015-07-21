using UnityEngine;

namespace MS
{
	public class TileView : View<Tile>
	{
        public GameObject FertilePrairiePrefab;
        public GameObject FertileForestPrefab;
        public GameObject FertileMountainPrefab;
        public GameObject FertileWaterPrefab;

        public GameObject BarrenPrairiePrefab;
		public GameObject BarrenForestPrefab;
        public GameObject BarrenMountainPrefab;
        public GameObject BarrenWaterPrefab;

		public GameObject DesertPrairiePrefab;
		public GameObject DesertForestPrefab;
        public GameObject DesertMountainPrefab;
        public GameObject DesertWaterPrefab;

		public GameObject FrozenPrairiePrefab;
		public GameObject FrozenForestPrefab;
        public GameObject FrozenMountainPrefab;
        public GameObject FrozenWaterPrefab;

		public GameObject VolcanicPrairiePrefab;
		public GameObject VolcanicForestPrefab;
        public GameObject VolcanicMountainPrefab;
        public GameObject VolcanicWaterPrefab;

        private GameObject m_InstancedTile;

        public override void UpdateView()
        {
            GameObject tileToInstantiate;

            if (m_InstancedTile != null)
			{
                Destroy(m_InstancedTile);
            }

            tileToInstantiate = FertileWaterPrefab;

            switch (m_Model.Type)
			{
				case Tile.ETerrain.Fertile:
					switch (m_Model.Surface)
					{
						case Tile.ESurface.Prairie:
                            tileToInstantiate = FertilePrairiePrefab;
                            break;
						case Tile.ESurface.Forest:
                            tileToInstantiate = FertileForestPrefab;
                            break;
						case Tile.ESurface.Mountain:
                            tileToInstantiate = FertileMountainPrefab;
                            break;
						case Tile.ESurface.Water:
                            tileToInstantiate = FertileWaterPrefab;
                            break;
					}
					break;
				case Tile.ETerrain.Barren:
					switch (m_Model.Surface)
					{
						case Tile.ESurface.Prairie:
                            tileToInstantiate = BarrenPrairiePrefab;
                            break;
						case Tile.ESurface.Forest:
                            tileToInstantiate = BarrenForestPrefab;
                            break;
						case Tile.ESurface.Mountain:
                            tileToInstantiate = BarrenMountainPrefab;
                            break;
						case Tile.ESurface.Water:
                            tileToInstantiate = BarrenWaterPrefab;
                            break;
					}
					break;
				case Tile.ETerrain.Desert:
					switch (m_Model.Surface)
					{
						case Tile.ESurface.Prairie:
                            tileToInstantiate = DesertPrairiePrefab;
                            break;
						case Tile.ESurface.Forest:
                            tileToInstantiate = DesertForestPrefab;
                            break;
						case Tile.ESurface.Mountain:
                            tileToInstantiate = DesertMountainPrefab;
                            break;
						case Tile.ESurface.Water:
                            tileToInstantiate = DesertWaterPrefab;
                            break;
					}
					break;
				case Tile.ETerrain.Frozen:
					switch (m_Model.Surface)
					{
						case Tile.ESurface.Prairie:
                            tileToInstantiate = FrozenPrairiePrefab;
                            break;
						case Tile.ESurface.Forest:
                            tileToInstantiate = FrozenForestPrefab;
                            break;
						case Tile.ESurface.Mountain:
                            tileToInstantiate = FrozenMountainPrefab;
                            break;
						case Tile.ESurface.Water:
                            tileToInstantiate = FrozenWaterPrefab;
                            break;
					}
					break;
				case Tile.ETerrain.Volcanic:
					switch (m_Model.Surface)
					{
						case Tile.ESurface.Prairie:
                            tileToInstantiate = VolcanicPrairiePrefab;
                            break;
						case Tile.ESurface.Forest:
                            tileToInstantiate = VolcanicForestPrefab;
                            break;
						case Tile.ESurface.Mountain:
                            tileToInstantiate = VolcanicMountainPrefab;
                            break;
						case Tile.ESurface.Water:
                            tileToInstantiate = VolcanicWaterPrefab;
                            break;
					}
					break;
            }

            m_InstancedTile = Instantiate(tileToInstantiate) as GameObject;
			m_InstancedTile.transform.SetParent(this.gameObject.transform);
            m_InstancedTile.transform.localPosition = Vector3.zero;
        }
	}
}
