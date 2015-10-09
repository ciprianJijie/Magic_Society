using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

namespace MS.Model.World
{
    public class Region : ModelElement, IEnumerable<Area>
    {
        protected static readonly int RANDOM_FERTILE            =   35;
        protected static readonly int RANDOM_BARREN             =   60;
        protected static readonly int RANDOM_DESERT             =   75;
        protected static readonly int RANDOM_FROZEN             =   90;
        protected static readonly int RANDOM_VOLCANIC           =   100;
        protected static readonly int RANDOM_FERTILE_ADJACENCY  =   50;
        protected static readonly int RANDOM_BARREN_ADJACENCY   =   33;
        protected static readonly int RANDOM_DESERT_ADJACENCY   =   25;
        protected static readonly int RANDOM_FROZEN_ADJACENCY   =   50;
        protected static readonly int RANDOM_VOLCANIC_ADJACENCY =   33;
        protected static readonly int RANDOM_FERTILE_PLAINS     =   25;
        protected static readonly int RANDOM_FERTILE_HILLS      =   35;
        protected static readonly int RANDOM_FERTILE_MOUNTAIN   =   50;
        protected static readonly int RANDOM_FERTILE_FOREST     =   85;
        protected static readonly int RANDOM_FERTILE_WATER      =   100;
        protected static readonly int RANDOM_BARREN_PLAINS      =   55;
        protected static readonly int RANDOM_BARREN_HILLS       =   85;
        protected static readonly int RANDOM_BARREN_MOUNTAIN    =   999;
        protected static readonly int RANDOM_BARREN_FOREST      =   95;
        protected static readonly int RANDOM_BARREN_WATER       =   100;
        protected static readonly int RANDOM_DESERT_PLAINS      =   70;
        protected static readonly int RANDOM_DESERT_HILLS       =   999;
        protected static readonly int RANDOM_DESERT_MOUNTAIN    =   90;
        protected static readonly int RANDOM_DESERT_FOREST      =   95;
        protected static readonly int RANDOM_DESERT_WATER       =   100;
        protected static readonly int RANDOM_FROZEN_PLAINS      =   10;
        protected static readonly int RANDOM_FROZEN_HILLS       =   20;
        protected static readonly int RANDOM_FROZEN_MOUNTAIN    =   60;
        protected static readonly int RANDOM_FROZEN_FOREST      =   90;
        protected static readonly int RANDOM_FROZEN_WATER       =   100;
        protected static readonly int RANDOM_VOLCANIC_PLAINS    =   20;
        protected static readonly int RANDOM_VOLCANIC_HILLS     =   40;
        protected static readonly int RANDOM_VOLCANIC_MOUNTAIN  =   100;
        protected static readonly int RANDOM_VOLCANIC_FOREST    =   999;
        protected static readonly int RANDOM_VOLCANIC_WATER     =   999;
        
        public Vector3          CubePosition;

        protected List<Area>    m_Areas;
        protected Area          m_Capital;

        public Area CapitalArea
        {
            get
            {
                return m_Capital;
            }
        }

        public Region()
        {
            m_Areas = new List<Area>(6);

            for (int i = 0; i < m_Areas.Capacity; i++)
            {
                //m_Areas[i] = new Area();
                m_Areas.Add(new Area());
            }

            m_Capital = new Area();
        }

        public void Randomize()
        {
            // Capital
            m_Capital.TerrainType = RandomTerrain();
            m_Capital.TopographyType = RandomTopography(m_Capital.TerrainType);

            // Peripheral
            for (int i = 1; i < m_Areas.Count; i++)
            {
                m_Areas[i].TerrainType = RandomTerrain(m_Capital.TerrainType);
                m_Areas[i].TopographyType = RandomTopography(m_Areas[i].TerrainType);
            }
        }

        public override void FromJSON(JSONNode json)
        {
            base.FromJSON(json);

            JSONArray array;

            array = json["areas"].AsArray;

            m_Capital.FromJSON(json["capital"]);

            for (int i = 0; i < array.Count; i++)
            {
                m_Areas[i].FromJSON(array[i]);
            }
        }

        public override JSONNode ToJSON()
        {
            JSONNode root;
            JSONArray array;

            root = base.ToJSON();
            array = new JSONArray();

            root.Add("capital", m_Capital.ToJSON());

            for (int i = 0; i < m_Areas.Count; i++)
            {
                array.Add(m_Areas[i].ToJSON());
            }

            root.Add("areas", array);

            return root;
        }

        public IEnumerator<Area> GetEnumerator()
        {
            return m_Areas.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_Areas.GetEnumerator();
        }

        protected Area.ETerrainType RandomTerrain()
        {
            int roll;

            roll = Tools.DiceBag.Roll(1, 100, 0);

            if (roll < RANDOM_FERTILE)
            {
                return Area.ETerrainType.Fertile;
            }
            else if (roll < RANDOM_BARREN)
            {
                return Area.ETerrainType.Barren;
            }
            else if (roll < RANDOM_DESERT)
            {
                return Area.ETerrainType.Desert;
            }
            else if (roll < RANDOM_FROZEN)
            {
                return Area.ETerrainType.Frozen;
            }
            else
            {
                return Area.ETerrainType.Volcanic;
            }
        }

        protected Area.ETerrainType RandomTerrain(Area.ETerrainType adjacentTerrain)
        {
            int adjacencyProb;
            int roll;
            
            switch (adjacentTerrain)
            {
                case Area.ETerrainType.Fertile:
                    adjacencyProb = RANDOM_FERTILE_ADJACENCY;
                    break;
                case Area.ETerrainType.Barren:
                    adjacencyProb = RANDOM_BARREN_ADJACENCY;
                    break;
                case Area.ETerrainType.Desert:
                    adjacencyProb = RANDOM_DESERT_ADJACENCY;
                    break;
                case Area.ETerrainType.Frozen:
                    adjacencyProb = RANDOM_FROZEN_ADJACENCY;
                    break;
                default:
                    adjacencyProb = RANDOM_VOLCANIC_ADJACENCY;
                    break;
            }

            roll = Tools.DiceBag.Roll(1, 100, 0);

            if (roll < adjacencyProb)
            {
                return adjacentTerrain;
            }

            return RandomTerrain();

        }

        protected Area.ETopographyType RandomTopography(Area.ETerrainType terrain)
        {
            int roll;

            roll = Tools.DiceBag.Roll(1, 100, 0);

            switch (terrain)
            {
                case Area.ETerrainType.Fertile:
                    if (roll < RANDOM_FERTILE_PLAINS)
                    {
                        return Area.ETopographyType.Forest;
                    }
                    else if (roll < RANDOM_FERTILE_HILLS)
                    {
                        return Area.ETopographyType.Hills;
                    }
                    else if (roll < RANDOM_FERTILE_MOUNTAIN)
                    {
                        return Area.ETopographyType.Mountain;
                    }
                    else if (roll < RANDOM_FERTILE_FOREST)
                    {
                        return Area.ETopographyType.Forest;
                    }
                    else if (roll < RANDOM_FERTILE_WATER)
                    {
                        return Area.ETopographyType.Water;
                    }
                    break;

                case Area.ETerrainType.Barren:
                    if (roll < RANDOM_BARREN_PLAINS)
                    {
                        return Area.ETopographyType.Forest;
                    }
                    else if (roll < RANDOM_BARREN_HILLS)
                    {
                        return Area.ETopographyType.Hills;
                    }
                    else if (roll < RANDOM_BARREN_MOUNTAIN)
                    {
                        return Area.ETopographyType.Mountain;
                    }
                    else if (roll < RANDOM_BARREN_FOREST)
                    {
                        return Area.ETopographyType.Forest;
                    }
                    else if (roll < RANDOM_BARREN_WATER)
                    {
                        return Area.ETopographyType.Water;
                    }
                    break;

                case Area.ETerrainType.Desert:
                    if (roll < RANDOM_DESERT_PLAINS)
                    {
                        return Area.ETopographyType.Forest;
                    }
                    else if (roll < RANDOM_DESERT_HILLS)
                    {
                        return Area.ETopographyType.Hills;
                    }
                    else if (roll < RANDOM_DESERT_MOUNTAIN)
                    {
                        return Area.ETopographyType.Mountain;
                    }
                    else if (roll < RANDOM_DESERT_FOREST)
                    {
                        return Area.ETopographyType.Forest;
                    }
                    else if (roll < RANDOM_DESERT_WATER)
                    {
                        return Area.ETopographyType.Water;
                    }
                    break;

                case Area.ETerrainType.Frozen:
                    if (roll < RANDOM_FROZEN_PLAINS)
                    {
                        return Area.ETopographyType.Forest;
                    }
                    else if (roll < RANDOM_FROZEN_HILLS)
                    {
                        return Area.ETopographyType.Hills;
                    }
                    else if (roll < RANDOM_FROZEN_MOUNTAIN)
                    {
                        return Area.ETopographyType.Mountain;
                    }
                    else if (roll < RANDOM_FROZEN_FOREST)
                    {
                        return Area.ETopographyType.Forest;
                    }
                    else if (roll < RANDOM_FROZEN_WATER)
                    {
                        return Area.ETopographyType.Water;
                    }
                    break;

                case Area.ETerrainType.Volcanic:
                    if (roll < RANDOM_VOLCANIC_PLAINS)
                    {
                        return Area.ETopographyType.Forest;
                    }
                    else if (roll < RANDOM_VOLCANIC_HILLS)
                    {
                        return Area.ETopographyType.Hills;
                    }
                    else if (roll < RANDOM_VOLCANIC_MOUNTAIN)
                    {
                        return Area.ETopographyType.Mountain;
                    }
                    else if (roll < RANDOM_VOLCANIC_FOREST)
                    {
                        return Area.ETopographyType.Forest;
                    }
                    else if (roll < RANDOM_VOLCANIC_WATER)
                    {
                        return Area.ETopographyType.Water;
                    }
                    break;                    
            }

            return Area.ETopographyType.Plains;
        }
    }
}
