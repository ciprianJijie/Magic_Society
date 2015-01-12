using UnityEngine;
using System;
using System.Collections;
using MS.Model;
using MS.Core;

namespace MS.Manager
{
    public class GameManager : Singleton<GameManager>
    {
        #region Attributes

        private static  Game    m_game;

        public MS.View.MapView  m_map;

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
            string filePath;

            filePath        =   MS.Utils.Path.ToScenario(scenarioName);
            m_game          =   new Game();
            m_game.Scenario =   new Scenario(filePath);

            // TODO: Remove after testing
            Instance.m_map.BindTo(m_game.Scenario.Map);
            Instance.m_map.UpdateView();
            // ---
        }

        #endregion


    }	
}
