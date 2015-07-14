using UnityEngine;

namespace MS
{
	public class TileView : View<Tile>
	{
        public Material FertileMaterial;
        public Material BarrenMaterial;
        public Material DesertMaterial;
        public Material FrozenMaterial;
        public Material VolcanicMaterial;

        [SerializeField]
        protected MeshRenderer  TerrainRenderer;

        public override void UpdateView()
        {
            Material terrainMaterial;

            switch (m_Model.Type)
            {
                case Tile.EType.Fertile:
                    terrainMaterial = FertileMaterial;
                    break;
                case Tile.EType.Barren:
                    terrainMaterial = BarrenMaterial;
                    break;
                case Tile.EType.Desert:
                    terrainMaterial = DesertMaterial;
                    break;
                case Tile.EType.Frozen:
                    terrainMaterial = FrozenMaterial;
                    break;
                case Tile.EType.Volcanic:
                    terrainMaterial = VolcanicMaterial;
                    break;
                default:
                    terrainMaterial = FertileMaterial;
                    break;
            }

            TerrainRenderer.sharedMaterial = terrainMaterial;
        }
	}
}

