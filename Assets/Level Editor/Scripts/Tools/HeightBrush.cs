using UnityEngine;
using MS.Model;

namespace MS
{
    public class HeightBrush : Brush
    {
        [RangeAttribute(-3, 3)]
        public int Height;

        public override void Draw(int x, int y, Grid grid)
        {
            grid.GetTile(x, y).Height = Height;
        }
    }
}
