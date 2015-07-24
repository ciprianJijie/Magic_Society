using UnityEngine;

namespace MS
{
	public class TileView : View<Tile>
	{
        public GameObject FertilePrairiePrefab;
        public GameObject FertileForestPrefab;
        public GameObject FertileMountainPrefab;
        public GameObject FertileWaterPrefab;
        public Elevation  FertileElevationPrefab;

        public GameObject BarrenPrairiePrefab;
		public GameObject BarrenForestPrefab;
        public GameObject BarrenMountainPrefab;
        public GameObject BarrenWaterPrefab;
        public Elevation BarrenElevationPrefab;

        public GameObject DesertPrairiePrefab;
		public GameObject DesertForestPrefab;
        public GameObject DesertMountainPrefab;
        public GameObject DesertWaterPrefab;
        public Elevation DesertElevationPrefab;

        public GameObject FrozenPrairiePrefab;
		public GameObject FrozenForestPrefab;
        public GameObject FrozenMountainPrefab;
        public GameObject FrozenWaterPrefab;
        public Elevation FrozenElevationPrefab;

        public GameObject VolcanicPrairiePrefab;
		public GameObject VolcanicForestPrefab;
        public GameObject VolcanicMountainPrefab;
        public GameObject VolcanicWaterPrefab;
        public Elevation VolcanicElevationPrefab;

        private GameObject m_InstancedTile;
        private GameObject m_InstancedElevation;

        public override void UpdateView()
        {
            GameObject tileToInstantiate;
            Elevation elevationToInstantiate;

            if (m_InstancedTile != null)
			{
                Destroy(m_InstancedTile);
            }

			// Default prefabs
            tileToInstantiate 		= 	FertileWaterPrefab;
            elevationToInstantiate 	= 	FertileElevationPrefab;

            switch (m_Model.Type)
			{
				case Tile.ETerrain.Fertile:
                    elevationToInstantiate = FertileElevationPrefab;
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
                    elevationToInstantiate = BarrenElevationPrefab;
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
                    elevationToInstantiate = DesertElevationPrefab;
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
                    elevationToInstantiate = FrozenElevationPrefab;
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
                    elevationToInstantiate = VolcanicElevationPrefab;
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

            m_InstancedTile.transform.position += Vector3.up * m_Model.Height * 0.5f;

            // Elevation
            if (m_InstancedElevation != null)
			{
                Destroy(m_InstancedElevation);
            }

            m_InstancedElevation = Instantiate(elevationToInstantiate.gameObject) as GameObject;

			m_InstancedElevation.transform.SetParent(this.gameObject.transform);
			m_InstancedElevation.transform.localPosition = Vector3.zero;
			m_InstancedElevation.GetComponent<Elevation>().ChangeHeight(m_Model.Height);
        }
	}
}
