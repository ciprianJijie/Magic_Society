using UnityEngine;

namespace MS.Core
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T m_instance;

        /**
      Returns the instance of this singleton.
   */
        public static T Instance
        {
            get
            {
                if(m_instance == null)
                {
                    m_instance = (T) FindObjectOfType(typeof(T));

                    if (m_instance == null)
                    {
                        MS.Debug.Core.LogError("An instance of " + typeof(T) + 
                            " is needed in the scene, but there is none.");
                    }
                }

                return m_instance;
            }
        }

        void Awake()
        {
            if (m_instance != null)
            {
                throw new AlreadyInstantiated(this);
            }

            m_instance = this as T;

            MS.Debug.Core.Log("Singleton for " + typeof(T) + " initialised.");
        }
    }
}
