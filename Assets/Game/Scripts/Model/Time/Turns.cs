using UnityEngine;
using System.Collections.Generic;

namespace MS.Model
{
    /// <summary>
    /// Controls the turns of the game.
    /// </summary>
    public class Turns : ModelElement
    {
        protected   List<Turn>  m_Turns;
        protected   int         m_CurrentTurn;

        private     int         m_TurnCounter;

        public MS.Events.PlayerEvent    OnRecollectionPhase     =   MS.Events.DefaultAction;
        public MS.Events.PlayerEvent    OnUpkeepPhase           =   MS.Events.DefaultAction;
        public MS.Events.PlayerEvent    OnMainPhase             =   MS.Events.DefaultAction;
        public MS.Events.Event          OnAllTurnsFinished      =   MS.Events.DefaultAction;

        public Turn CurrentTurn
        {
            get
            {
                return m_Turns[m_CurrentTurn];
            }
        }

        public int TurnCounter
        {
            get
            {
                return m_TurnCounter;
            }
        }

        public Turns(IEnumerable<Player> players)
        {
            Turn turn;

            m_Turns = new List<Turn>();

            foreach (Player player in players)
            {
                turn = new Turn(player);

                turn.OnTurnFinished += OnTurnFinished;

                m_Turns.Add(turn);
            }

            m_CurrentTurn = -1;
        }

        public void NextTurn()
        {
            m_CurrentTurn++;

            if (m_CurrentTurn >= m_Turns.Count)
            {
                m_TurnCounter++;
                m_CurrentTurn = 0;

                OnAllTurnsFinished();

                m_Turns[0].Start();
            }
            else
            {
                m_Turns[m_CurrentTurn].Start();
            }

            Turn currentTurn;

            currentTurn = m_Turns[m_CurrentTurn];

            if (currentTurn.CurrentPhase is UpkeepPhase)
            {
                OnUpkeepPhase(currentTurn.Player);
            }
            else if (currentTurn.CurrentPhase is RecollectionPhase)
            {
                OnRecollectionPhase(CurrentTurn.Player);
            }
            else if (currentTurn.CurrentPhase is MainPhase)
            {
                OnMainPhase(currentTurn.Player);
            }
        }

        public override void FromJSON(SimpleJSON.JSONNode json)
        {
            throw new System.NotImplementedException();
        }

        public override SimpleJSON.JSONNode ToJSON()
        {
            throw new System.NotImplementedException();
        }

        protected void OnTurnFinished()
        {
            NextTurn();
        }
    }
}