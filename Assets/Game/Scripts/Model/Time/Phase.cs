using UnityEngine;
using SimpleJSON;

namespace MS.Model
{
    public abstract class Phase : ModelElement
    {
        public string Name;
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
            Name    =   json["name"];
            Player  =   GameController.Instance.Game.Players.Find(json["player"]);
        }

        public override JSONNode ToJSON()
        {
            JSONClass root;

            root = new JSONClass();

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