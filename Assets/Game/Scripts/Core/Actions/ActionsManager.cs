using UnityEngine;
using System.Collections;

namespace MS.Core.Actions
{
    public class ActionsManager : Singleton<ActionsManager>
    {
        public Transform    Container;

        public T Create<T>() where T: Action
        {
            GameObject obj = new GameObject();
            T action;

            obj.transform.SetParent(Container);

            action = obj.AddComponent<T>();

            obj.name = action.ToString();

            return action;
        }
    }
}
