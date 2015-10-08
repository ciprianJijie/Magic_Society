using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MS.Model.World
{
    public class World : ModelElement, IEnumerable<Region>
    {
        protected List<Region> m_Regions;

        protected int m_Range;
        

        public World(int range)
        {
            m_Range     =   range;
            m_Regions   =   new List<Region>();
        }

        /// <summary>
        /// Generates a random world with random regions.
        /// </summary>
        public void GenerateRandom()
        {

            Region region;

            //for (int x = (-m_Range + 1); x < m_Range; x++)
            //{
            //    for (int y = (-m_Range + 1); y < m_Range; y++)
            //    {
            //        for (int z = (-m_Range + 1); z < m_Range; z++)
            //        {
            //            region = new Region();

            //            region.CubePosition = new Vector3(x, y, z);
            //            region.Randomize();
            //            m_Regions.Add(region);
            //        }
            //    }                
            //}

            List<Vector3> positions = new List<Vector3>();
            int z;

            for (int x = -m_Range; x <= m_Range; x++)
            {
                for (int y = (int)(Mathf.Max(-m_Range, -x - m_Range)); y <= (int)(Mathf.Min(m_Range, -x + m_Range)); y++)
                {
                    z = -x - y;
                    positions.Add(new Vector3(x, y, z));
                }
            }

            foreach (Vector3 position in positions)
            {
                region              =   new Region();
                region.CubePosition =   position;

                region.Randomize();
                m_Regions.Add(region);
            }
        }

        protected void AddRegion(Region region, Vector2 boardPosition)
        {
            m_Regions[CalculateListIndex(boardPosition)] = region;
        }

        protected int CalculateListIndex(Vector3 cubePosition)
        {
            return CalculateListIndex((int)cubePosition.x, (int)cubePosition.y, (int)cubePosition.z);
        }

        protected int CalculateListIndex(int x, int y, int z)
        {
            Vector2 axial = Hexagon.CubeToAxial(x, y, z);

            int offsetX;
            int offsetY;

            offsetX = (int)axial.x + m_Range;
            offsetY = (int)axial.y + m_Range + (int)Mathf.Min(0, axial.x);

            return offsetX + offsetY;
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
