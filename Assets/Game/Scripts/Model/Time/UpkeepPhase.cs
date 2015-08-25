using UnityEngine;
using SimpleJSON;

namespace MS.Model
{
    public class UpkeepPhase : Phase
    {
        public UpkeepPhase()
        {
            Name = "UPKEEP_PHASE";
        }

        public override void Execute()
        {
            // TODO: Pay units, buildings, etc
            UnityEngine.Debug.Log("Paying upkeeps of player " + Player.Name);

            Finish();
        }
    }
}