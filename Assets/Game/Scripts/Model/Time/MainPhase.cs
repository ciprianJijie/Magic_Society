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
            // TODO: Allow the player to move, build, etc

            UnityEngine.Debug.Log("Entering in the main phase and waiting for " + Player.Name + " player to finish moving.");
        }
    }
}