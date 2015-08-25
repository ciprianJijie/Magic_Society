using UnityEngine;
using SimpleJSON;

namespace MS.Model
{
    public class RecollectionPhase : Phase
    {
        public RecollectionPhase()
        {
            Name = "RECOLLECTION_PHASE";
        }

        public override void Execute()
        {
            // TODO: Recollect resources

            UnityEngine.Debug.Log("Recollecting resources for player " + Player.Name);

            Finish();
        }
    }
}