using UnityEngine;
using SimpleJSON;

namespace MS.Model
{
    public abstract class Phase : ModelElement
    {
        public Player Player;

        public MS.Events.Event OnStarted = MS.Events.DefaultAction;
        public MS.Events.Event OnFinished = MS.Events.DefaultAction;

        public abstract void Execute();

        public void Finish()
        {
            OnFinished();
        }

        public override void FromJSON(JSONNode json)
        {
            base.FromJSON(json);

            Player  =   GameController.Instance.Game.Players.Find(json["player"]);
        }

        public override JSONNode ToJSON()
        {
            JSONNode root;

            root = base.ToJSON();

            root.Add("name", Name);
            root.Add("player", Player.Name);

            return root;
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