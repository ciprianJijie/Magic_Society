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

            End();
        }
    }
}