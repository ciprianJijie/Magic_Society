using UnityEngine;
using System.Collections;

namespace MS
{
    public class Brush
    {
        public int              Radius;
        public Tile.ETerrain    Terrain;
        public Tile.ESurface    Surface;
        public int              Height;

        public Brush()
        {
            Radius  = 1;
            Terrain = Tile.ETerrain.Fertile;
            Surface = Tile.ESurface.Prairie;
            Height  = 0;
        }

        public void Draw(Tile tileToDraw)
        {
            tileToDraw.Type     =   Terrain;
            tileToDraw.Surface  =   Surface;
            tileToDraw.Height   =   Height;
        }
    }
}
