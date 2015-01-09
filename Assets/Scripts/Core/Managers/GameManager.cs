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
            StartGame("TestScenario");
        }

        protected void OnFinish()
        {

        }

        #endregion

        #region Public methods

        public static void StartGame(string scenarioName)
        {
            m_game              =   new Game();
            m_currentScenario   =   new Scenario();

            m_currentScenario.Load(MS.Utils.Path.ToScenario("TestScenario"));

            m_game.Scenario = m_currentScenario;
        }

        #endregion

        #region Attributes

        private static  GameManager     m_instance;
        private static  Game            m_game;
        private static  Scenario        m_currentScenario;

        #endregion
    }	
}
