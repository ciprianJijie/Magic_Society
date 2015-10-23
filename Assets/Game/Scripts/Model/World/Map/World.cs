using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MS.ExtensionMethods;
using System.Linq;

namespace MS.Model.World
{
    public class World : ModelElement, IEnumerable<Region>
    {
        protected Dictionary<Vector3, Region>              m_Regions;

        protected int m_Range;

        public int TileCount
        {
            get
            {
                return m_Regions.Count;
            }
        }

        public int Range
        {
            get
            {
                return m_Range;
            }
        }

        public World(int range)
        {
            m_Range     =   range;
            //m_Regions   =   new List<Region>();
        }

        /// <summary>
        /// Generates a random world with random regions.
        /// </summary>
        public void GenerateRandom()
        {
            Region          region;
            List<Vector3>   positions;

            positions   =   new List<Vector3>(Hexagon.CalculateTilesForRange(m_Range));
            m_Regions   =   new Dictionary<Vector3, Region>();

            foreach (Vector3 position in positions)
            {
                region              =   new Region();
                region.CubePosition =   position;
                region.Owner        =   Game.Instance.Players.Find("NEUTRAL_PLAYER");

                region.Randomize();

                m_Regions.Add(position, region);
            }
        }

        public MapElement FindElement(Player owner, string name)
        {
            var ownedRegions = m_Regions.Values.ToList().FindAll(i => i.Owner == owner);

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
            foreach (Region region in m_Regions.Values.ToList().FindAll(i => i.Owner == owner))
            {
                foreach (MapElement element in region.GetOwnedElements(owner))
                {
                    yield return element;
                }
            }
        }

        public Region GetRegion(int x, int y, int z)
        {
            return GetRegion(new Vector3(x, y, z));
        }

        public Region GetRegion(Vector3 cubePosition)
        {
            Region region;

            m_Regions.TryGetValue(cubePosition, out region);

            return region;
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

        public IEnumerator<Region> GetEnumerator()
        {
            return m_Regions.Values.ToList().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_Regions.GetEnumerator();
        }
    }
}
