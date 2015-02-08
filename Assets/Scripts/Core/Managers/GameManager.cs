using UnityEngine;
using System;
using System.Collections;
using MS.Model;
using MS.Core;
using SimpleJSON;

namespace MS.Manager
{
    public class GameManager : Singleton<GameManager>
    {
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
            string      filePath;
            TextAsset   jsonFile;
            string      jsonText;
            JSONNode    json;

            filePath        =   MS.Utils.Path.ToScenario(scenarioName);
            jsonFile        =   (TextAsset)Resources.Load(filePath, typeof(TextAsset));
            jsonText        =   jsonFile.text;
            json            =   JSON.Parse(jsonText);
            m_game          =   new Game();
            m_game.Scenario =   new Scenario(json);

            // TODO: Remove after testing
            Instance.m_mapView.BindTo(m_game.Scenario.Map);
            Instance.m_mapView.UpdateView();
            // ---
        }

        /// <summary>
        /// Searchs for a player with the given name that is playing the game in this scenario.
        /// </summary>
        /// <returns>The player.</returns>
        /// <param name="name">Name of the player to search for.</param>
        public static MS.Model.Player GetPlayer(string name)
        {
            for (int playerIndex = 0; playerIndex < m_game.Scenario.Players.Length; ++playerIndex)
            {
                if (m_game.Scenario.Players[playerIndex].Name == name)
                {
                    return m_game.Scenario.Players[playerIndex];
                }
            }
            throw new Exceptions.PlayerNotFound(name);
        }

        #endregion

        #region Attributes and properties

        public static Game Game
        {
            get
            {
                return m_game;
            }
        }

        private static  Game    m_game;

        public MS.View.MapView  m_mapView;

        #endregion
    }
}
