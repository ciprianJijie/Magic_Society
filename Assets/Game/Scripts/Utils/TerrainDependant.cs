using UnityEngine;
using MS.Model;

namespace MS
{
	public class TerrainDependant : MonoBehaviour
	{
	    public GameObject FertilePrefab;
	    public GameObject BarrenPrefab;
	    public GameObject DesertPrefab;
	    public GameObject FrozenPrefab;
	    public GameObject VolcanicPrefab;

	    protected GameObject m_InstantiatedPrefab;

        public GameObject SelectPrefab(Tile.Terrain terrainType)
        {
            switch (terrainType)
            {
                case Tile.Terrain.Fertile:
                    return FertilePrefab;
                case Tile.Terrain.Barren:
                    return BarrenPrefab;
                case Tile.Terrain.Desert:
                    return DesertPrefab;
                case Tile.Terrain.Frozen:
                    return FrozenPrefab;
                case Tile.Terrain.Volcanic:
                    return VolcanicPrefab;
                default:
                    return null;
            }
        }

		public GameObject UpdateObject(ModelElement element, Tile.Terrain terrainType)
		{
			if (m_InstantiatedPrefab != null)
			{
                Destroy(m_InstantiatedPrefab);
            }

            GameObject prefab;

            prefab = FertilePrefab;

			switch(terrainType)
			{
				case Tile.Terrain.Fertile:
                    prefab = FertilePrefab;
					break;
				case Tile.Terrain.Barren:
                    prefab = BarrenPrefab;
					break;
				case Tile.Terrain.Desert:
                    prefab = DesertPrefab;
					break;
				case Tile.Terrain.Frozen:
                    prefab = FrozenPrefab;
					break;
				case Tile.Terrain.Volcanic:
                    prefab = VolcanicPrefab;
					break;
            }

            m_InstantiatedPrefab = Utils.Instantiate(prefab, this.transform, this.transform.position, this.transform.rotation);

            foreach (IUpdatableView view in m_InstantiatedPrefab.GetComponents<IUpdatableView>())
            {
                view.UpdateView();
            }

            foreach (IUpdatableView<City> view in m_InstantiatedPrefab.GetComponents<IUpdatableView<City>>())
            {
                view.UpdateView(element as City);
            }

            return m_InstantiatedPrefab;
        }
	}
}
