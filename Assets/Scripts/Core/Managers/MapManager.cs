using UnityEngine;
using System;
using MS.Core;

namespace MS.Managers
{
    public class MapManager : Singleton<MapManager>
    {
        void Start()
        {
            MS.Debug.Core.Log("Image for grass is " + MS.Managers.ResourceManager.GetSprite(MS.Model.Tile.TerrainType.Grass));
        }
    }
}

