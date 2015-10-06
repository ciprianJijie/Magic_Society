using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MS.Model.Map
{
    public class World : ModelElement, IEnumerable<Region>
    {
        protected List<Region> m_Regions;

        protected int m_HorizontalSize;
        protected int m_VerticalSize;
        
        public World(int horizontalSize, int verticalSize)
        {
            m_HorizontalSize    =   horizontalSize;
            m_VerticalSize      =   verticalSize;
            m_Regions           =   new List<Region>(horizontalSize * verticalSize);
        }

        protected void AddRegion(Region region, Vector2 boardPosition)
        {
            m_Regions[CalculateListIndex(boardPosition)] = region;
        }

        protected int CalculateListIndex(Vector2 boardPosition)
        {
            return CalculateListIndex((int)boardPosition.x, (int)boardPosition.y);
        }

        protected int CalculateListIndex(int x, int y)
        {
            return (x * m_HorizontalSize) + y;
        }

        public IEnumerator<Region> GetEnumerator()
        {
            return m_Regions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_Regions.GetEnumerator();
        }
    }
}
