using UnityEngine;
using System.Collections;

namespace MS
{
    public class Brush
    {
        public int              Radius;
        public Tile.EType       Terrain;
        public Tile.ESurface    Surface;

        public Brush()
        {
            Radius  = 1;
            Terrain = Tile.EType.Fertile;
            Surface = Tile.ESurface.Prairie;
        }

        public void Draw(Tile tileToDraw)
        {
            tileToDraw.Type     =   Terrain;
            tileToDraw.Surface  =   Surface;
        }
    }
}
