using UnityEngine;
using System;
using MS.Core;

namespace MS.Managers
{
    public class ResourceManager : Singleton<ResourceManager>
    {

        #region Attributes

        public Sprite GrassTile;
        public Sprite ForestTile;
        public Sprite MountainTile;
        public Sprite WaterTile;
        public Sprite NoneTile;

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
    }
}

