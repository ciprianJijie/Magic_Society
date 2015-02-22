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
        /// List of players currently playing the game.
        ///</summary>
        private List<Player>    m_players;

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
                return Instance.m_players[Instance.m_currentPlayerIndex];
            }
        }

        ///<summary>
        /// Triggers the end of the current turn and starts the next one
        ///</summary>
        public static void NextTurn()
        {
            if (OnTurnEnded != null)
            {
                OnTurnEnded(CurrentPlayer, Instance.m_turnCount);
            }

            Instance.m_currentPlayerIndex = (Instance.m_currentPlayerIndex + 1) % Instance.m_players.Count;
            Instance.m_turnCount++;

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
            m_players               =   new List<Player>();
            m_currentPlayerIndex    =   0;
            m_turnCount             =   0;
        }
    }
}
