
namespace MS
{
    public class TerrainBrush : Brush
    {
        public Tile.Terrain TerrainType;

        public override void Draw(Tile tileToDraw)
        {
            tileToDraw.TerrainType = TerrainType;
        }
    }
}
