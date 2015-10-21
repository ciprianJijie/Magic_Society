using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MS.ExtensionMethods;

namespace MS.Model.World
{
    public class World : ModelElement, IEnumerable<Region>
    {
        protected List<Region>              m_Regions;

        protected int m_Range;
        
        public int HorizontalSize
        {
            get
            {
                return 0;
            }
        }
        
        public int VerticalSize
        {
            get
            {
                return 0;
            }
        } 

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

                region.Owner = Game.Instance.Players.Find("NEUTRAL_PLAYER");

                m_Regions.Add(region);
            }
        }

        public MapElement FindElement(Player owner, string name)
        {
            var ownedRegions = m_Regions.FindAll(i => i.Owner == owner);

            foreach (Region region in ownedRegions)
            {
                foreach (MapElement element in region.GetOwnedElements(owner))
                {
                    if (element.Name == name)
                    {
                        return element;
                    }
                }
            }

            return null;
        }

        public IEnumerable<MapElement> FindElements(Player owner)
        {
            foreach (Region region in m_Regions.FindAll(i => i.Owner == owner))
            {
                foreach (MapElement element in region.GetOwnedElements(owner))
                {
                    yield return element;
                }
            }
        }

        public Region GetRegion(int x, int y, int z)
        {
            return m_Regions[CalculateListIndex(x, y, z)];
        }

        public Region GetRegion(Vector3 cubePosition)
        {
            return GetRegion((int)cubePosition.x, (int)cubePosition.y, (int)cubePosition.z);
        }

        public Region GetRegion(int x, int y)
        {
            Vector3 cube;

            cube = Hexagon.OffsetToCube(x, y);

            return GetRegion(cube);
        }

        public Region GetRegion(Vector2 position)
        {
            return GetRegion((int)position.x, (int)position.y);
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
