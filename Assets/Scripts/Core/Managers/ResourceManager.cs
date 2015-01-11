using UnityEngine;
using System;

namespace MS.Managers
{
    public class ResourceManager : MonoBehaviour
    {

        #region Properties

        public static ResourceManager Instance
        {
            get
            {
                if (m_instance == null)
                {
                    throw new NoInstance(null);
                }
                return m_instance;
            }
        }

        #endregion

        #region MonoBehaviour methods

        void Awake()
        {
            if (m_instance != null)
            {
                throw new AlreadyInstantiated(this);
            }
            MS.Debug.Core.Log("Resources Manager singleton instantiated.");

            m_instance = this;
        }

        #endregion

        #region Public Methods

        public static Sprite GetSprite(MS.Model.Tile.TerrainType terrainType)
        {
            switch (terrainType)
            {
                case MS.Model.Tile.TerrainType.Grass:       return Instance.GrassTile;
                case MS.Model.Tile.TerrainType.Forest:      return Instance.ForestTile;
                case MS.Model.Tile.TerrainType.Mountain:    return Instance.MountainTile;
                case MS.Model.Tile.TerrainType.Water:       return Instance.WaterTile;

                default:                                    return Instance.NoneTile;
            }
        }

        #endregion

        #region Attributes

        public Sprite GrassTile;
        public Sprite ForestTile;
        public Sprite MountainTile;
        public Sprite WaterTile;
        public Sprite NoneTile;

        private static ResourceManager m_instance;

        #endregion
    }
}

