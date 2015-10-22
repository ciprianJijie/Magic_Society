using UnityEngine;
using System.Collections.Generic;

namespace MS.Model
{
    /// <summary>
    /// Controls the turns of the game.
    /// </summary>
    public class Turns : ModelElement
    {
        protected   List<Turn>      m_Turns;
        protected   int             m_CurrentTurn;

        private     int             m_TurnCounter;

        public      Events.Event    OnAllTurnsFinished  =   Events.DefaultAction;
        public      Events.Event    OnFirstPlayerTurn   =   Events.DefaultAction;

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

                m_Turns.Add(turn);
            }
        }

        public void Start()
        {
            m_CurrentTurn = 0;
            OnFirstPlayerTurn();
        }

        public void Execute()
        {
            if (m_CurrentTurn >= m_Turns.Count)
            {
                End();
            }
            else
            {
                m_Turns[m_CurrentTurn].OnFinished += OnTurnFinished;
                m_Turns[m_CurrentTurn].Start();
                m_Turns[m_CurrentTurn].Execute();
            }
        }

        public void AdvanceTurn()
        {
            m_CurrentTurn++;
        }

        public void End()
        {
            m_TurnCounter++;
            OnAllTurnsFinished();
            Start();            
            Execute();
        }

        public Phase FindPhase<T>(Player player) where T: Phase
        {
            var turn = m_Turns.Find(i => i.Player == player);

            if (turn == null)
            {
                UnityEngine.Debug.LogError("No turn for player " + player);
            }

            return turn.FindPhase<T>();
        }

        protected void OnTurnFinished(Turn turn)
        { 
            m_Turns[m_CurrentTurn].OnFinished -= OnTurnFinished;
            AdvanceTurn();

            Execute();
        }
    }
}