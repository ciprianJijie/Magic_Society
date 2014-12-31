using UnityEngine;
using System;
using System.Collections;
using MS.Model;

namespace MS.Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Properties
        public static GameManager Instance
        {
            get
            {
                if (m_instance == null)
                {
                    throw new NoInstance(null);
                }
                return m_instance;
            }
        }

        #endregion

        #region Monobehaviour methods
        void Awake()
        {
            if (m_instance != null)
            {
                throw new AlreadyInstantiated(this);
            }
            MS.Debug.Core.Log("Game Manager singleton instantiated.");

            m_instance = this;

            OnStart();
        }

        void OnDestroy()
        {
            OnFinish();
        }

        #endregion

        #region Events methods

        protected void OnStart()
        {
        }

        protected void OnFinish()
        {
        }

        #endregion

        #region Attributes

        private static  GameManager     m_instance;

        #endregion
    }	
}
