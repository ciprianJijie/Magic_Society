
namespace MS
{
    public abstract class Brush
    {
        public int Radius;

        public abstract void Draw(Tile tileToDraw, int Radius);
    }
}
