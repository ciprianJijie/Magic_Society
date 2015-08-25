using UnityEngine;
using SimpleJSON;

namespace MS.Model
{
    public class Turn : ModelElement
    {
        public      Player      Player;

        protected   Phase[]     m_Phases;
        protected   int         m_CurrentPhase;

        public MS.Events.Event  OnTurnStarted    =   MS.Events.DefaultAction;
        public MS.Events.Event  OnTurnFinished   =   MS.Events.DefaultAction;

        public Phase CurrentPhase
        {
            get
            {
                return m_Phases[m_CurrentPhase];
            }
        }

        public Turn(Player player)
        {
            Player              =   player;
            m_Phases            =   new Phase[3];
            m_Phases[0]         =   new RecollectionPhase();
            m_Phases[1]         =   new UpkeepPhase();
            m_Phases[2]         =   new MainPhase();
            m_Phases[0].Player  =   Player;
            m_Phases[1].Player  =   Player;
            m_Phases[2].Player  =   Player;

            m_Phases[0].OnFinished += OnPhaseFinished;
            m_Phases[1].OnFinished += OnPhaseFinished;
            m_Phases[2].OnFinished += OnPhaseFinished;
        }

        public void Start()
        {
            m_CurrentPhase = 0;

            m_Phases[m_CurrentPhase].Execute();

            OnTurnStarted();
        }

        public void NextPhase()
        {
            m_CurrentPhase++;

            if (m_CurrentPhase >= m_Phases.Length)
            {
                m_CurrentPhase = 0;
                Finish();
            }
            else
            {
                m_Phases[m_CurrentPhase].Execute();
            }
        }

        public void Finish()
        {
            OnTurnFinished();
        }

        public override void FromJSON(JSONNode json)
        {
            Player          =   GameController.Instance.Game.Players.Find(json["player"]);
            m_CurrentPhase  =   json["current_phase"].AsInt;
        }

        public override JSONNode ToJSON()
        {
            JSONClass root;

            root = new JSONClass();

            root.Add("player", Player.Name);
            root.Add("current_phase", new JSONData(m_CurrentPhase));

            return root;
        }

        protected void OnPhaseFinished()
        {
            NextPhase();
        }
    }
}
