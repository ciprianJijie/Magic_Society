using UnityEngine;

namespace MS
{
	public static class Utils
	{
		public static T Instantiate<T>(T prefab, Transform parent, Vector3 position, Quaternion rotation) where T : MonoBehaviour
		{
            T           instantiated;
            GameObject  instantiatedObj;

            instantiatedObj     =   GameObject.Instantiate(prefab.gameObject, position, rotation) as GameObject;
            instantiated        =   instantiatedObj.GetComponent<T>();

            instantiated.transform.SetParent(parent);

            return instantiated;
		}

        public static GameObject Instantiate(GameObject prefab, Transform parent, Vector3 position, Quaternion rotation)
        {
            GameObject instantiatedObj;

            instantiatedObj     =   GameObject.Instantiate(prefab.gameObject, position, rotation) as GameObject;

            instantiatedObj.transform.SetParent(parent);

            return instantiatedObj;
        }

        public static Color HexToRGB(string hex)
        {
            byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            return new Color32(r, g, b, 255);
        }

        public static string ColorToHex(Color32 color)
        {
            string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
            return hex;
        }
    }
}