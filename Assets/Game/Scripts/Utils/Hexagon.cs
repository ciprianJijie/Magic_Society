using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        public static float Height(float hexagonSize)
        {
            return hexagonSize * 2.0f;
        }

        /// <summary>
        /// Calculates the width of an hexagon given its size.
        /// </summary>
        /// <param name="hexagonSize">Distance from the hexagon's center to any of its corners.</param>
        /// <return>Width of the hexagon.</return>
        public static float Width(float hexagonSize)
        {
            return Mathf.Sqrt(3) / 2.0f * Height(hexagonSize);
        }

        // Conversions

        public static Vector2 WorldToAxial(float x, float y, float z, float hexagonSize)
        {
            float column;
            float row;

            column = (x * (Mathf.Sqrt(3f) / 3f) - (z / 3f)) / hexagonSize;
            row = z * (2f / 3f) / hexagonSize;

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

        public static Vector2 CubeToAxial(Vector3 cubePoint)
        {
            return new Vector2(cubePoint.x, cubePoint.z);
        }

        public static Vector3 AxialToCube(Vector2 axial)
        {
            return AxialToCube(axial.x, axial.y);
        }

        public static Vector3 AxialToCube(float x, float y)
        {
            float cubeX = x;
            float cubeZ = y;
            float cubeY = - cubeX - cubeZ;

            return new Vector3 (cubeX, cubeY, cubeZ);
        }

        public static Vector2 AxialToWorld(int column, int row, float hexagonSize)
        {
            float x;
            float y;

            x = hexagonSize * Mathf.Sqrt(3) * (column + row / 2.0f);
            y = hexagonSize * (3f / 2f) * row;

            return new Vector2(x, y);
        }

        public static Vector2 CubeToWorld(Vector3 cube, float hexagonSize)
        {
            return CubeToWorld((int)cube.x, (int)cube.y, (int)cube.z, hexagonSize);
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

        public static Vector3[] GetCubeNeighbors(Vector3 cube)
        {
            return GetCubeNeighbors((int)cube.x, (int)cube.y, (int)cube.z);
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
            directions[3] = new Vector2(-1f, 0f);
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

        public static Vector3 RoundToCube (Vector3 cubePos)
        {
            Vector3 pos;

            int rx = Mathf.RoundToInt (cubePos.x);
            int ry = Mathf.RoundToInt (cubePos.y);
            int rz = Mathf.RoundToInt (cubePos.z);

            float x_diff = Mathf.Abs (rx - cubePos.x);
            float y_diff = Mathf.Abs (ry - cubePos.y);
            float z_diff = Mathf.Abs (rz - cubePos.z);

            if (x_diff > y_diff && x_diff > z_diff)
            {
                rx = -ry - rz;
            }
            else if (y_diff > z_diff)
            {
                ry = -rx - rz;
            }
            else
            {
                rz = -rx - ry;
            }

            pos = new Vector3 (rx, ry, rz);

            return pos;
        }

        public static float Distance(Vector3 cubeA, Vector3 cubeB)
        {
            return Distance(cubeA.x, cubeA.y, cubeA.z, cubeB.x, cubeB.y, cubeB.z);
        }

        public static float Distance(float x1, float y1, float z1, float x2, float y2, float z2)
        {
            return Mathf.Max(Mathf.Abs(x1 - x2), Mathf.Abs(y1 - y2), Mathf.Abs(z1 - z2));
        }

        public static float Distance(Vector2 axialA, Vector2 axialB)
        {
            Vector3 cubeA;
            Vector3 cubeB;

            cubeA = Hexagon.AxialToCube(axialA);
            cubeB = Hexagon.AxialToCube(axialB);

            return Distance(cubeA, cubeB);
        }

        public static IEnumerable<Vector3> TilesInRange(Vector2 axial, int range)
        {
            return TilesInRange((int)axial.x, (int)axial.y, range);
        }

        public static IEnumerable<Vector3> TilesInRange(int axialX, int axialY, int range)
        {
            Vector3 cube;

            cube = AxialToCube(axialX, axialY);

            return TilesInRange(cube, range);
        }

        public static IEnumerable<Vector3> TilesInRange(Vector3 cube, int range)
        {
            return TilesInRange((int)cube.x, (int)cube.y, (int)cube.z, range);
        }

        public static IEnumerable<Vector3> TilesInRange(int cubeX, int cubeY, int cubeZ, int range)
        {
            for (int dx = -range; dx <= range; dx++)
            {
                for (int dy = -range; dy <= range; dy++)
                {
                    for (int dz = -range; dz <= range; dz++)
                    {
                        if (dx + dy + dz == 0)
                        {
                            yield return new Vector3(cubeX + dx, cubeY + dy, cubeZ + dz);
                        }
                    }
                }
            }
        }

        public static IEnumerable<Vector3> CalculateTilesForRange(int range)
        {
            List<Vector3> positions = new List<Vector3>();
            int z;

            for (int x = -range; x <= range; x++)
            {
                for (int y = (int)(Mathf.Max(-range, -x - range)); y <= (int)(Mathf.Min(range, -x + range)); y++)
                {
                    z = -x - y;
                    positions.Add(new Vector3(x, y, z));
                }
            }

            return positions;
        }
    }
}
