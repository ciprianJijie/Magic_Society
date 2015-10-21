using UnityEngine;
using SimpleJSON;

namespace MS.Model
{
    public class Turn : ModelElement
    {
        public      Player      Player;

        protected   Phase[]     m_Phases;
        protected   int         m_CurrentPhase;

        public Events.TurnEvent      OnStarted       =   Events.DefaultAction;
        public Events.TurnEvent      OnFinished      =   Events.DefaultAction;
        
        public Phase CurrentPhase
        {
            get
            {
                return m_Phases[m_CurrentPhase];
            }
        }

        public Turn(Player player)
        {
            Name                =   "TURN";
            Player              =   player;
            m_Phases            =   new Phase[4];
            m_Phases[0]         =   new RecollectionPhase();
            m_Phases[1]         =   new UpkeepPhase();
            m_Phases[2]         =   new BuildingPhase();
            m_Phases[3]         =   new MainPhase();
            m_Phases[0].Player  =   Player;
            m_Phases[1].Player  =   Player;
            m_Phases[2].Player  =   Player;
            m_Phases[3].Player  =   Player;
        }

        public void Start()
        {
            m_CurrentPhase = 0;

            OnStarted(this);
        }

        public void Execute()
        {
            if (m_CurrentPhase >= m_Phases.Length)
            {
                End();
            }
            else
            {
                m_Phases[m_CurrentPhase].OnFinished += OnPhaseFinished;
                m_Phases[m_CurrentPhase].Start();
                m_Phases[m_CurrentPhase].Execute();
            }
        }

        public void AdvancePhase()
        {
            m_CurrentPhase++;            
        }

        public void End()
        {
            m_CurrentPhase = 0;
            OnFinished(this);
        }

        public Phase FindPhase<T>() where T: Phase
        {
            for (int i = 0; i < m_Phases.Length; i++)
            {
                if (m_Phases[i] is T)
                {
                    return m_Phases[i];
                }
            }

            return null;
        }

        protected void OnPhaseFinished(Phase phase)
        {
            m_Phases[m_CurrentPhase].OnFinished -= OnPhaseFinished;
            AdvancePhase();

            Execute();
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", Name, Player.Name);
        }

        public override JSONNode ToJSON()
        {
            JSONClass root;

            root = new JSONClass();

            root.Add("player", Player.Name);
            root.Add("current_phase", new JSONData(m_CurrentPhase));

            return root;
        }
    }
}
