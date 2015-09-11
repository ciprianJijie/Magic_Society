using UnityEngine;
using SimpleJSON;

namespace MS.Model
{
    public class MainPhase : Phase
    {
        public MainPhase()
        {
            Name = "MAIN_PHASE";
        }

        public override void Execute()
        {
            UnityEngine.Debug.Log(Name + " for " + Player.Name + " started");
            Player.Play<MainPhase>(this);
        }
    }
}