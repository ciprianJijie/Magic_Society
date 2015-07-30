using UnityEngine;

namespace MS
{
    public class MapElementView : View<MapElement>
    {
        public TerrainDependant     ForestPrefab;

        public float                VerticalOffset;

        private TerrainDependant    m_InstantiatedElement;

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

            Vector3 verticalOffset;

            verticalOffset = this.transform.up * tile.Height * VerticalOffset;

            if (Model is Forest)
            {
                m_InstantiatedElement = Utils.Instantiate<TerrainDependant>(ForestPrefab, this.transform, this.transform.position + verticalOffset, this.transform.rotation);
            }

            m_InstantiatedElement.UpdateObject(tile.TerrainType);
        }

        /// <summary>
        /// Adjusts the vertical position of the element. This is used to make elemnts appear at the same height as the tile they are.
        /// </summary>
        /// <param name="verticalOffset">Vertical offset to be applied to the element.</param>
        public void UpdateHeight(float verticalOffset)
        {
            m_InstantiatedElement.transform.position += this.transform.up * verticalOffset;
        }
    }
}
