using UnityEngine;
using MS.Model;
using System;

namespace MS.Views.World
{
    public class GenericElementView : TerrainDependantView<Model.MapElement>
    {
        protected GameObject m_Element;

        public override void UpdateView(MapElement element)
        {
            GameObject prefab;
            Vector3 offset;
            Tile tile;

            prefab      =   GetPrefabByTerrain(TerrainType);
            tile        =   GameController.Instance.Game.Map.Grid.GetTile(element.X, element.Y);
            offset      =   Vector3.zero;
            offset.y    =   (tile.Height * 0.5f);

            if (m_Element != null)
            {
                Destroy(m_Element.gameObject);
            }

            if (prefab != null)
            {
                m_Element = Utils.Instantiate(prefab, this.transform, this.transform.position + offset, this.transform.rotation);
            }
        }
    }
}
