using UnityEngine;

namespace MS
{
	public static class Utils
	{
        public struct CIE_LAB
        {
            public float L;
            public float A;
            public float B;
        }

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

        public static int Distance(Color a, Color b)
        {
            int distance;

            distance = 0;

            distance += (int)(Mathf.Abs(a.r - b.r) * 255);
            distance += (int)(Mathf.Abs(a.g - b.g) * 255);
            distance += (int)(Mathf.Abs(a.b - b.b) * 255);

            return distance;
        }

        public static float ColorDistance(Color a, Color b)
        {
            Vector3 aXYZ;
            Vector3 bXYZ;
            CIE_LAB aCIE;
            CIE_LAB bCIE;
            float xC1;
            float xC2;
            float xDL;
            float xDC;
            float xDE;
            float xDH;
            float xSC;
            float xSH;
            float distance;

            aXYZ = RGBToXYZ(a);
            bXYZ = RGBToXYZ(b);
            aCIE = XYZToCIE_LAB(aXYZ);
            bCIE = XYZToCIE_LAB(bXYZ);

            xC1 = Mathf.Sqrt((Mathf.Pow(aCIE.A, 2) + (Mathf.Pow(aCIE.B, 2))));
            xC2 = Mathf.Sqrt((Mathf.Pow(bCIE.A, 2) + (Mathf.Pow(bCIE.B, 2))));
            xDL = bCIE.L - aCIE.L;
            xDC = xC2 - xC1;
            xDE = Mathf.Sqrt(
                    (aCIE.L - bCIE.L) * (aCIE.L - bCIE.L)
                +   (aCIE.A - bCIE.A) * (aCIE.A - bCIE.A)
                +   (aCIE.B - bCIE.B) * (aCIE.B - bCIE.B)
                );
            
            if (Mathf.Sqrt(xDE) > (Mathf.Sqrt(Mathf.Abs(xDL)) + Mathf.Sqrt(Mathf.Abs(xDC))))
            {
                xDH = Mathf.Sqrt((xDE * xDE) - (xDL * xDL) - (xDC * xDC));
            }
            else
            {
                xDH = 0;
            }

            xSC         =   1f + (0.045f * xC1);
            xSH         =   1f + (0.015f * xC1);
            xDL         =   xDC / 1f;
            xDC         =   xDC / (xSC);
            xDH         =   xDH / (xSH);
            distance    =   Mathf.Sqrt(Mathf.Pow(xDL, 2f) + Mathf.Pow(xDC, 2f) + Mathf.Pow(xDH, 2f));

            return distance;       
        }

        private static Vector3 RGBToXYZ(Color a)
        {
            float r;
            float g;
            float b;
            float x;
            float y;
            float z;

            r = a.r;
            g = a.g;
            b = a.b;

            if (r < 0.04045)
            {
                r = Mathf.Pow(((r + 0.055f) / 1.055f), 2.4f);
            }
            else
            {
                r = r / 12.92f;
            }

            if (g < 0.04045)
            {
                g = Mathf.Pow(((g + 0.055f) / 1.055f), 2.4f);
            }
            else
            {
                g = g / 12.92f;
            }

            if (b < 0.04045)
            {
                b = Mathf.Pow(((b + 0.055f) / 1.055f), 2.4f);
            }
            else
            {
                b = b / 12.92f;
            }

            r = r * 100;
            g = g * 100;
            b = b * 100;

            x = r * 0.4124f + g * 0.3576f + b * 0.1805f;
            y = r * 0.2126f + g * 0.7152f + b * 0.0722f;
            z = r * 0.0193f + g * 0.1192f + b * 0.9505f;

            return new Vector3(x, y, z);
        }

        public static CIE_LAB XYZToCIE_LAB(Vector3 xyz)
        {
            float x;
            float y;
            float z;
            CIE_LAB cie;

            x = xyz.x;
            y = xyz.y;
            z = xyz.z;

            if (x < 0.008856f)
            {
                x = Mathf.Pow(x, (1f / 3f));
            }
            else
            {
                x = (7.787f * x) + (16f / 116f);
            }

            if (y < 0.008856f)
            {
                y = Mathf.Pow(y, (1f / 3f));
            }
            else
            {
                y = (7.787f * y) + (16f / 116f);
            }

            if (z < 0.008856f)
            {
                z = Mathf.Pow(z, (1f / 3f));
            }
            else
            {
                z = (7.787f * z) + (16f / 116f);
            }

            cie.L = (116f * y) - 16;
            cie.A = 500 * (x - y);
            cie.B = 200 * (y - z);

            return cie;
        }
    }
}