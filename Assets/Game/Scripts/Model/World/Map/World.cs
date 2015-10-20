using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MS.Model.World
{
    public class World : ModelElement, IEnumerable<Region>
    {
        protected List<Region>      m_Regions;
        protected List<OwnableMapElement>  m_Elements;

        protected int m_Range;
        

        public World(int range)
        {
            m_Range     =   range;
            m_Regions   =   new List<Region>();
            m_Elements  =   new List<OwnableMapElement>();
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
            return m_Elements.Find(i => i.Name == name && i.Owner == owner);
        }

        public IEnumerable<OwnableMapElement> FindElements(Player owner)
        {
            return m_Elements.FindAll(i => i.Name == Name && i.Owner == owner);
        }

        public Region GetRegion(int x, int y)
        {
            Vector3 cube;

            cube = Hexagon.OffsetToCube(x, y);

            return m_Regions[CalculateListIndex(cube)];
        }

        public Region GetRegion(Vector2 position)
        {
            return GetRegion((int)position.x, (int)position.y);
        }

        public MapElement GetElement(int x, int y)
        {
            throw new System.NotImplementedException();
        }

        public MapElement GetElement(Vector2 position)
        {
            return GetElement((int)position.x, (int)position.y);
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
