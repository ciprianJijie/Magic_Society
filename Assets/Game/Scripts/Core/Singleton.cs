using UnityEngine;


public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T m_Instance;

    /**
  Returns the instance of this singleton.
*/
    public static T Instance
    {
        get
        {
            if(m_Instance == null)
            {
                m_Instance = (T) FindObjectOfType(typeof(T));

                if (m_Instance == null)
                {
                    throw new System.NullReferenceException("An instance of " + typeof(T) +
                        " is needed in the scene, but there is none.");
                }
            }

            return m_Instance;
        }
    }

    void Awake()
    {
        if (m_Instance != null)
        {
			Destroy(this.gameObject);
            throw new AlreadyInstantiatedException(this);
        }

        m_Instance = this as T;

        DontDestroyOnLoad(this.gameObject);
    }
}
