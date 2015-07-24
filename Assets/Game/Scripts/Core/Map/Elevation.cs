using UnityEngine;
using System.Collections.Generic;

namespace MS
{
	public class Elevation : MonoBehaviour
	{
	    public GameObject ElevationPrefab;
	    public float VerticalOffset;

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

	        GameObject elevation;
	        float y;

	        for (int i = 0; i < newHeight; ++i)
			{
	            y = i * VerticalOffset;
	            elevation = Instantiate(ElevationPrefab);
				elevation.transform.SetParent(this.gameObject.transform);
				elevation.transform.localPosition = Vector3.zero;
				elevation.transform.position += Vector3.up * y;

	            m_InstantiatedElevations.Add(elevation);
	        }
		}
	}
}
