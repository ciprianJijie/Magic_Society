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
	}
}