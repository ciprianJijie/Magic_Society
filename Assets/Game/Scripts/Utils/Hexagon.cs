using UnityEngine;

namespace MS
{
    public static class Hexagon
    {
        // Hexagonal grid operations

        /// <summary>
        /// Calculates the position of a corner for a given hexagon.
        /// </summary>
        /// <param name="hexagonCenter">Position in world coordinates of the center of the hexagon.</param>
        /// <param name="hexagonSize">Distance from the center of the hexagon to any of its corners.</param>
        /// <param name="cornerIndex">Index of the corner, from 0 to 5.</param>
        /// <return>World position of the corner.</return>
        public static Vector2 CornerPosition(Vector2 hexagonCenter, float hexagonSize, int cornerIndex)
        {
            Vector2     pos;
            float       angleDegree;
            float       angleRadians;

            angleDegree = 60 * cornerIndex + 30;
            angleRadians = Mathf.PI / 180 * angleDegree;
            pos.x = hexagonCenter.x + hexagonSize * Mathf.Cos(angleRadians);
            pos.y = hexagonCenter.y + hexagonSize * Mathf.Sin(angleRadians);

            return pos;
        }

        /// <summary>
        /// Calculates the height of an hexagon given its size.
        /// </summary>
        /// <param name="hexagonSize">Distance from the hexagon's center to any of its corners.</param>
        /// <return>Height of the hexagon.</return>
        public static float HexagonHeight(float hexagonSize)
        {
            return hexagonSize * 2.0f;
        }

        /// <summary>
        /// Calculates the width of an hexagon given its size.
        /// </summary>
        /// <param name="hexagonSize">Distance from the hexagon's center to any of its corners.</param>
        /// <return>Width of the hexagon.</return>
        public static float HexagonWidth(float hexagonSize)
        {
            return Mathf.Sqrt(3) / 2.0f * HexagonHeight(hexagonSize);
        }

        // Conversions

        public static Vector2 WorldToAxial(float x, float y, float z, float hexagonSize)
        {
            float column;
            float row;

            column = (x * (Mathf.Sqrt(3f) / 3f) - (y / 3f)) / hexagonSize;
            row = y * (2f / 3f) / hexagonSize;

            return new Vector2(Mathf.RoundToInt(column), Mathf.RoundToInt(row));
        }

        /// <summary>
        /// Converts from Cube to Axial coordinate system.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <return>Position converted to Axial coordinate system.</return>
        public static Vector2 CubeToAxial(int x, int y, int z)
        {
            return new Vector2(x, z);
        }

        public static Vector3 AxialToCube(int column, int row)
        {
            float x;
            float y;
            float z;

            x = column;
            y = row;
            z = - x - y;

            return new Vector3(x, y, z);
        }

        public static Vector2 AxialToWorld(int column, int row, float hexagonSize)
        {
            float x;
            float y;

            x = hexagonSize * Mathf.Sqrt(3) * (column + row / 2.0f);
            y = hexagonSize * (3f / 2f) * row;

            return new Vector2(x, y);
        }

        public static Vector2 CubeToWorld(int x, int y, int z, float hexagonSize)
        {
            Vector2 axial;

            axial = CubeToAxial(x, y, z);

            return AxialToWorld((int)axial.x, (int)axial.y, hexagonSize);
        }

        public static Vector2 CubeToOffset(int x, int y, int z)
        {
            return new Vector2(x + (z + (z & 1)) / 2.0f, z);
        }

        public static Vector3 OffsetToCube(int column, int row)
        {
            float x;
            float y;
            float z;

            x = column - (row - (row & 1)) / 2.0f;
            z = row;
            y = - x - z;

            return new Vector3(x, y, z);
        }

        public static Vector2 OffsetToWorld(int column, int row, float hexagonSize)
        {
            float x;
            float y;

            x = hexagonSize * Mathf.Sqrt(3f) * (column - 0.5f * (row & 1));
            y = hexagonSize * (3f / 2f) * row;

            return new Vector2(x, y);
        }

        public static Vector3[] GetCubeNeighbors(int x, int y, int z)
        {
            Vector3[]   neighbors;
            Vector3[]   directions;
            Vector3     position;

            position    =   new Vector3(x, y, z);
            neighbors   =   new Vector3[6];
            directions  =   new Vector3[6];

            directions[0] = new Vector3(1f, -1f, 0f);
            directions[1] = new Vector3(1f, 0f, -1f);
            directions[2] = new Vector3(0f, 1f, -1f);
            directions[3] = new Vector3(-1f, 1f, 0f);
            directions[4] = new Vector3(-1f, 0f, 1f);
            directions[5] = new Vector3(0f, -1f, 1f);

            neighbors[0] = position + directions[0];
            neighbors[1] = position + directions[1];
            neighbors[2] = position + directions[2];
            neighbors[3] = position + directions[3];
            neighbors[4] = position + directions[4];
            neighbors[5] = position + directions[5];

            return neighbors;
        }

        public static Vector2[] GetAxialNeighbors(int column, int row)
        {
            Vector2[]   directions;
            Vector2[]   neighbors;
            Vector2     position;

            position    =   new Vector2(column, row);
            directions  =   new Vector2[6];
            neighbors   =   new Vector2[6];

            directions[0] = new Vector2(1f, 0f);
            directions[1] = new Vector2(1f, -1f);
            directions[2] = new Vector2(0f, -1f);
            directions[3] = new Vector2(1f, 0f);
            directions[4] = new Vector2(-1f, 1f);
            directions[5] = new Vector2(0f, 1f);

            neighbors[0] = position + directions[0];
            neighbors[1] = position + directions[1];
            neighbors[2] = position + directions[2];
            neighbors[3] = position + directions[3];
            neighbors[4] = position + directions[4];
            neighbors[5] = position + directions[5];

            return neighbors;
        }

        public static Vector2[] GetOffsetNeighbors(int column, int row)
        {
            Vector2[,]  directions;
            Vector2[]   neighbors;
            Vector2     position;
            int         parity;

            parity      =   row & 1;
            position    =   new Vector2(column, row);
            directions  =   new Vector2[2, 6];
            neighbors   =   new Vector2[6];

            directions[0, 0] = new Vector2(1f, 0f);
            directions[0, 1] = new Vector2(1f, -1f);
            directions[0, 2] = new Vector2(0f, -1f);
            directions[0, 3] = new Vector2(-1f, 0f);
            directions[0, 4] = new Vector2(0f, 1f);
            directions[0, 5] = new Vector2(1f, 1f);
            directions[1, 0] = new Vector2(1f, 0f);
            directions[1, 1] = new Vector2(0f, -1f);
            directions[1, 2] = new Vector2(-1f, -1f);
            directions[1, 3] = new Vector2(-1f, 0f);
            directions[1, 4] = new Vector2(-1f, 1f);
            directions[1, 5] = new Vector2(0f, +1f);

            neighbors[0] = position + directions[parity, 0];
            neighbors[1] = position + directions[parity, 1];
            neighbors[2] = position + directions[parity, 2];
            neighbors[3] = position + directions[parity, 3];
            neighbors[4] = position + directions[parity, 4];
            neighbors[5] = position + directions[parity, 5];

            return neighbors;
        }

        public static float CalculateCubeDistance(int aX, int aY, int aZ, int bX, int bY, int bZ)
        {
            return Mathf.Max(Mathf.Abs(aX - bX), Mathf.Abs(aY - bY), Mathf.Abs(aZ - bZ));
        }
    }
}
