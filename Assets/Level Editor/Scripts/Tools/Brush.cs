using UnityEngine;
using System.Collections.Generic;

namespace MS
{
    public abstract class Brush
    {
        public GridController GridController;
        public int Radius;

        public abstract void Draw(Tile tileToDraw);

        public void Draw(Vector2 axial, int radius)
        {
            Draw((int)axial.x, (int)axial.y, radius);
        }

        public virtual void Draw(int x, int y, int radius)
        {
            foreach (Vector3 cube in TilesInRadius(x, y, radius))
            {
                Draw(GridController.Grid.GetTile(Hexagon.CubeToAxial(cube)));
            }
        }

        public IEnumerable<Vector3> TilesInRadius(Vector2 axial, int radius)
        {
            return TilesInRadius((int)axial.x, (int)axial.y, radius);
        }

        public IEnumerable<Vector3> TilesInRadius(int x, int y, int radius)
        {
            IEnumerable<Vector3> tiles = new List<Vector3>();

            tiles = Hexagon.TilesInRange(x, y, radius);

            return tiles;
        }
    }
}
