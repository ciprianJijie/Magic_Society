using UnityEngine;
using System.Collections.Generic;

namespace MS
{
    public abstract class Brush
    {
        public int Radius;

        public void Draw(Vector2 axial, Grid grid)
        {
            Draw((int)axial.x, (int)axial.y, grid);
        }

        public abstract void Draw(int x, int y, Grid grid);


        public IEnumerable<Vector2> Draw(Vector2 axial, int radius, Grid grid)
        {
            return Draw((int)axial.x, (int)axial.y, radius, grid);
        }

        public virtual IEnumerable<Vector2> Draw(int x, int y, int radius, Grid grid)
        {
            Vector2 axial;

            foreach (Vector3 cube in TilesInRadius(x, y, radius))
            {
                axial = Hexagon.CubeToAxial(cube);

                Draw(axial, grid);

                yield return axial;
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
