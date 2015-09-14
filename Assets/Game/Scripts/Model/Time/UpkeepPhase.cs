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
            foreach (MapElement element in GameController.Instance.Game.Map.Grid.GetElements(Player))
            {
                IUpkeepMaintained toPay = element as IUpkeepMaintained;

                if (toPay != null)
                {
                    toPay.PayUpkeepCosts();
                }
            }

            Finish();
        }
    }
}