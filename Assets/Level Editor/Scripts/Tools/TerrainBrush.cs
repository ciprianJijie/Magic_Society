
namespace MS
{
    public class TerrainBrush : Brush
    {
        public Tile.Terrain TerrainType;

        public override void Draw(int x, int y, Grid grid)
        {
            grid.GetTile(x, y).TerrainType = TerrainType;
        }
    }
}
