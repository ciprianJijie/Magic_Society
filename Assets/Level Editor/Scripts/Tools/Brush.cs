using UnityEngine;
using System.Collections;

namespace MS
{
    public class Brush
    {
        public int              Radius;
        public Tile.Terrain     Terrain;
        public int              Height;

        public Brush()
        {
            Radius  = 1;
            Terrain = Tile.Terrain.Fertile;
            Height  = 0;
        }

        public void Draw(Tile tileToDraw)
        {
            tileToDraw.TerrainType  =   Terrain;
            tileToDraw.Height       =   Height;
        }
    }
}
