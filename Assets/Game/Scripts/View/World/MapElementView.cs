using UnityEngine;
using MS.Model;
using System;

namespace MS.Views
{
    public class MapElementView : View<MapElement>
    {
        public TerrainDependant     ForestPrefab;
        public TerrainDependant     StonePrefab;
        public TerrainDependant     GoldPrefab;
        public Controllers.Kingdom.CityController   CityPrefab;

        public float                VerticalOffset;

        private GameObject          m_InstantiatedElement;

        public override void UpdateView()
        {
            UnityEngine.Debug.LogWarning("MapElement.UpdateView() is deprecated. Use UpdateView(Tile tile) instead.");
        }

        public void UpdateView(Tile tile)
        {
            if (m_InstantiatedElement != null)
            {
                Destroy(m_InstantiatedElement.gameObject);
            }

            GameObject prefab = null;

            if (Model is Forest)
            {
                prefab = ForestPrefab.gameObject;
            }
            else if (Model is StoneDeposits)
            {
                prefab = StonePrefab.gameObject;
            }
            else if (Model is GoldDeposits)
            {
                prefab = GoldPrefab.gameObject;
            }
            else if (Model is City)
            {
                prefab = CityPrefab.gameObject;
            }

            if (prefab != null)
            {
                TerrainDependant                    terrainDependant;

                m_InstantiatedElement   =   Utils.Instantiate(prefab, this.transform, this.transform.position, this.transform.rotation);
                terrainDependant        =   m_InstantiatedElement.GetComponent<TerrainDependant>();

                if (terrainDependant != null)
                {
                    terrainDependant.UpdateObject(Model, tile.TerrainType);
                }
            }
        }

        /// <summary>
        /// Adjusts the vertical position of the element. This is used to make elemnts appear at the same height as the tile they are.
        /// </summary>
        /// <param name="verticalOffset">Vertical offset to be applied to the element.</param>
        public void UpdateHeight(float verticalOffset)
        {
            m_InstantiatedElement.transform.position = this.transform.position + this.transform.up * verticalOffset;
        }

        public override void UpdateView(MapElement element)
        {
            throw new NotImplementedException();
        }
    }
}
