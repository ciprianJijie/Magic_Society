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
            Player.Play<MainPhase>(this);
        }
    }
}