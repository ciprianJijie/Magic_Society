using UnityEngine;
using SimpleJSON;

namespace MS.Model
{
    public abstract class Phase : ModelElement
    {
        public Player Player;

        public Events.PhaseEvent OnStarted              =   Events.DefaultAction;
        public Events.PhaseEvent OnFinished             =   Events.DefaultAction;
        public static Events.PhaseEvent OnPhaseStarted  =   Events.DefaultAction;
        public static Events.PhaseEvent OnPhaseFinished =   Events.DefaultAction;

        public void Start()
        {
            OnStarted(this);
            OnPhaseStarted(this);
        }

        public abstract void Execute();

        public void End()
        {
            OnFinished(this);
            OnPhaseFinished(this);
        }

        public override void FromJSON(JSONNode json)
        {
            base.FromJSON(json);

            Player  =   Game.Instance.Players.Find(json["player"]);
        }

        public override JSONNode ToJSON()
        {
            JSONNode root;

            root = base.ToJSON();

            root.Add("name", Name);
            root.Add("player", Player.Name);

            return root;
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", Name, Player.Name);
        }

        // Static factory method

        public static Phase Create(string type)
        {
            Phase phase;

            phase = null;

            if (type == "Upkeep")
            {
                phase = new UpkeepPhase();
            }
            else if (type == "Recollection")
            {
                phase = new RecollectionPhase();
            }
            else if (type == "Main")
            {
                phase = new MainPhase();
            }

            return phase;
        }
    }
}