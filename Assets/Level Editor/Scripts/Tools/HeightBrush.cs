using UnityEngine;

namespace MS
{
    public class HeightBrush : Brush
    {
        [RangeAttribute(-3, 3)]
        public int Height;

        public override void Draw(Tile tileToDraw)
        {
            tileToDraw.Height = Height;
        }
    }
}
