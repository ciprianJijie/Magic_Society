using UnityEngine;
using System;

namespace MS.Manager
{
    public abstract class Manager : MonoBehaviour
    {
        public abstract Manager Instance
        {
            get;
        }

        void OnEnable()
        {
            if (m_instance != null)
            {
                throw new Exceptions.AlreadyInstantiated(this);
            }
            m_instance = this;

            MS.Debug.Core.Log(this + " manager singleton instantiated.");
        }

        public static Manager m_instance;
    }
}

