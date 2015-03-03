using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MS.Model;

namespace MS.Manager
{
    public class TurnManager : MS.Core.Singleton<TurnManager>
    {
        // Events
        public delegate void TurnEvent(Player player, int turnCount);

        ///<ssummary>
        /// Event triggered when a new turn starts
        ///</summary>
        public static event TurnEvent OnTurnStarted;

        ///<summary>
        /// Event triggered when a turn comes to an end
        ///</summary>
        public static event TurnEvent OnTurnEnded;

        ///<summary>
        /// Index of the current player playing this turn
        ///</summary>
        private int             m_currentPlayerIndex;

        ///<summary>
        /// Number of turn passed since the start of the game.
        ///</summary>
        private int             m_turnCount;

        public static MS.Model.Player CurrentPlayer
        {
            get
            {
                return Manager.GameManager.Game.Scenario.Players[Instance.m_currentPlayerIndex];
            }
        }

        public static void NextTurn()
        {
            Instance.AdvanceTurn();
        }


        ///<summary>
        /// Triggers the end of the current turn and starts the next one
        ///</summary>
        public void AdvanceTurn()
        {
            MS.Debug.Core.Log("Ending turn of " + CurrentPlayer);
            if (OnTurnEnded != null)
            {
                OnTurnEnded(CurrentPlayer, Instance.m_turnCount);
            }

            Instance.m_currentPlayerIndex = (Instance.m_currentPlayerIndex + 1) % Manager.GameManager.Game.Scenario.Players.Length;
            Instance.m_turnCount++;

            MS.Debug.Core.Log("Starting turn of " + CurrentPlayer);
            if (OnTurnStarted != null)
            {
                OnTurnStarted(CurrentPlayer, Instance.m_turnCount);
            }

            // TODO: Remove hack to skip AI turn
            if (CurrentPlayer is AIPlayer)
            {
                NextTurn();
            }
        }

        void Start()
        {
            m_currentPlayerIndex    =   0;
            m_turnCount             =   0;
        }
    }
}
