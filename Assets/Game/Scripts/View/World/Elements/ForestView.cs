using UnityEngine;
using MS.Model;

namespace MS.Views.World
{
    public class ForestView : TerrainDependantView<Model.Forest>
    {
        protected GameObject m_Forest;

        public override void UpdateView(Forest element)
        {
            GameObject prefab;

            prefab = GetPrefabByTerrain(TerrainType);

            if (m_Forest != null)
            {
                Destroy(m_Forest.gameObject);
            }

            if (prefab != null)
            {
                m_Forest = Utils.Instantiate(prefab, this.transform, this.transform.position, this.transform.rotation);
            }
        }
    }
}
