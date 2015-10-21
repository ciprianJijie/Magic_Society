using UnityEngine;

namespace MS.ExtensionMethods
{
    public static class Extensions
    {
        /// <summary>
        /// Swaps the y and z coordinates to fix problems between coordinate systems with Z as the UP axis instead of Y like Unity.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector3 SwappedYZ(this Vector3 v)
        {
            return new Vector3(v.x, v.z, v.y);
        }
    }
}
