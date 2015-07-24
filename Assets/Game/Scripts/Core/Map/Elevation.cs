using UnityEngine;
using System.Collections.Generic;

namespace MS
{
	public class Elevation : MonoBehaviour
	{
	    public GameObject   ElevationPrefab;
        public GameObject   DepresionPrefab;
        public Transform    Container;
        public Transform    Terrain;
	    public float        VerticalOffset;

	    protected List<GameObject> m_InstantiatedElevations;

	    public void ChangeHeight(int newHeight)
		{
			if (m_InstantiatedElevations == null)
			{
                m_InstantiatedElevations = new List<GameObject>(3);
            }

			foreach (GameObject obj in m_InstantiatedElevations)
			{
	            Destroy(obj);
	        }

			m_InstantiatedElevations.Clear();

            GameObject prefab;
	        GameObject elevation;
            float direction;
	        float y;

            if (newHeight > 0)
            {
                prefab = ElevationPrefab;
                direction = 1.0f;
            }
            else
            {
                prefab = DepresionPrefab;
                direction = -1.0f;
            }

	        for (int i = 0; i < Mathf.Abs(newHeight); ++i)
			{
	            y = i * VerticalOffset;
                elevation = Instantiate(prefab);
                elevation.transform.SetParent(Container);
                elevation.transform.localPosition = Vector3.zero;
                elevation.transform.position += Vector3.up * y * direction;
                
                m_InstantiatedElevations.Add(elevation.gameObject);
	        }

            Terrain.position += Vector3.up * newHeight * VerticalOffset;
		}
	}
}
