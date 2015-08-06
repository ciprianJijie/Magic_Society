using MS.Model;

namespace MS
{
    public class ObjectBrush : Brush
    {
        public string ElementName;

        public ObjectBrush()
        {
            ElementName = "None";
        }

        public override void Draw(int x, int y, Grid grid)
        {
            MapElement element = MapElement.Create(x, y, ElementName);

            grid.SetElement(x, y, element);
        }
    }
}