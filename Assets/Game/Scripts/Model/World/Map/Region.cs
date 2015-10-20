using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

namespace MS.Model.World
{
    public class Region : ModelElement, IEnumerable<Area>, IOwnable
    {        
        public Vector3          CubePosition;

        protected List<Area>    m_Areas;
        protected Area          m_Capital;
        protected City          m_City;
        protected Player        m_Owner;

        public Area CapitalArea
        {
            get
            {
                return m_Capital;
            }
        }

        public Player Owner
        {
            get
            {
                return m_Owner;
            }

            set
            {
                m_City.Owner    =   value;
                m_Owner         =   value;
            }
        }

        public Region()
        {
            m_Areas = new List<Area>(6);

            for (int i = 0; i < m_Areas.Capacity; i++)
            {
                m_Areas.Add(new Area());
            }

            m_Capital = new Area();
        }

        public void Randomize()
        {
            // Capital
            m_Capital.TerrainType = RandomTerrain();
            m_Capital.TopographyType = Area.ETopographyType.Plains; //RandomTopography(m_Capital.TerrainType);

            m_City = new City();

            // Peripheral
            for (int i = 0; i < m_Areas.Count; i++)
            {
                m_Areas[i].TerrainType = RandomTerrain(m_Capital.TerrainType);
                m_Areas[i].TopographyType = RandomTopography(m_Capital.TerrainType);
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

            roll = Tools.DiceBag.Roll(1, 8, 0);

            if (roll < 4) // 1-3
            {
                return Area.ETerrainType.Fertile;
            }
            else if (roll < 6) // 4-5
            {
                return Area.ETerrainType.Barren;
            }
            else if (roll < 7) // 6
            {
                return Area.ETerrainType.Desert;
            }
            else if (roll < 8) // 7
            {
                return Area.ETerrainType.Frozen;
            }
            else // 8
            {
                return Area.ETerrainType.Volcanic;
            }
        }

        protected Area.ETerrainType RandomTerrain(Area.ETerrainType adjacentTerrain)
        {
            int roll;

            roll = Tools.DiceBag.Roll(1, 8, 0);

            switch(adjacentTerrain)
            {
                case Area.ETerrainType.Fertile:
                    if (roll <5)
                    {
                        return Area.ETerrainType.Fertile;
                    }
                    else if (roll < 7)
                    {
                        return Area.ETerrainType.Barren;
                    }
                    else
                    {
                        return Area.ETerrainType.Frozen;
                    }

                case Area.ETerrainType.Barren:
                    if (roll < 3)
                    {
                        return Area.ETerrainType.Fertile;
                    }
                    else if (roll < 6)
                    {
                        return Area.ETerrainType.Barren;
                    }
                    else
                    {
                        return Area.ETerrainType.Desert;
                    }

                case Area.ETerrainType.Desert:
                    if (roll < 5)
                    {
                        return Area.ETerrainType.Barren;
                    }
                    else
                    {
                        return Area.ETerrainType.Desert;
                    }

                case Area.ETerrainType.Frozen:
                    if (roll < 5)
                    {
                        return Area.ETerrainType.Fertile;
                    }
                    else
                    {
                        return Area.ETerrainType.Frozen;
                    }

                case Area.ETerrainType.Volcanic:
                    if (roll < 5)
                    {
                        return Area.ETerrainType.Barren;
                    }
                    else
                    {
                        return Area.ETerrainType.Volcanic;
                    }
            }

            // Default value
            return Area.ETerrainType.Fertile;
        }

        protected Area.ETopographyType RandomTopography(Area.ETerrainType terrain)
        {
            int roll;

            roll = Tools.DiceBag.Roll(1, 8, 0);

            switch (terrain)
            {
                case Area.ETerrainType.Fertile:
                    if (roll < 5)
                    {
                        return Area.ETopographyType.Plains;
                    }
                    else if (roll < 6)
                    {
                        return Area.ETopographyType.Hills;
                    }
                    else if (roll < 7)
                    {
                        return Area.ETopographyType.Forest;
                    }
                    else if (roll < 8)
                    {
                        return Area.ETopographyType.Mountain;
                    }
                    else
                    {
                        return Area.ETopographyType.Water;
                    }

                case Area.ETerrainType.Barren:
                    if (roll < 3)
                    {
                        return Area.ETopographyType.Plains;
                    }
                    else if (roll < 5)
                    {
                        return Area.ETopographyType.Hills;
                    }
                    else if (roll < 6)
                    {
                        return Area.ETopographyType.Forest;
                    }
                    else if (roll < 8)
                    {
                        return Area.ETopographyType.Mountain;
                    }
                    else
                    {
                        return Area.ETopographyType.Water;
                    }


                case Area.ETerrainType.Desert:
                    if (roll < 6)
                    {
                        return Area.ETopographyType.Plains;
                    }
                    else if (roll < 8)
                    {
                        return Area.ETopographyType.Hills;
                    }
                    else
                    {
                        return Area.ETopographyType.Water;
                    }


                case Area.ETerrainType.Frozen:
                    if (roll < 3)
                    {
                        return Area.ETopographyType.Plains;
                    }
                    else if (roll < 5)
                    {
                        return Area.ETopographyType.Forest;
                    }
                    else if (roll < 8)
                    {
                        return Area.ETopographyType.Mountain;
                    }
                    else
                    {
                        return Area.ETopographyType.Water;
                    }


                case Area.ETerrainType.Volcanic:
                    if (roll < 2)
                    {
                        return Area.ETopographyType.Plains;
                    }
                    else if (roll < 5)
                    {
                        return Area.ETopographyType.Hills;
                    }
                    else if (roll < 8)
                    {
                        return Area.ETopographyType.Mountain;
                    }
                    else
                    {
                        return Area.ETopographyType.Water;
                    }
                                       
            }

            return Area.ETopographyType.Plains;
        }
    }
}
