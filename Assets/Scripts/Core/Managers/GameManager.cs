using UnityEngine;
using System;
using System.Collections;
using MS.Model;
using MS.Core;

namespace MS.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        #region Attributes

        private static  Game            m_game;
        private static  Scenario        m_currentScenario;

        #endregion

        #region Properties

        #endregion

        #region Monobehaviour methods
        void Start()
        {
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


    }	
}
