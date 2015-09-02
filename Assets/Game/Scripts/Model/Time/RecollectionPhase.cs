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
            // Search all cities

            foreach (MapElement element in GameController.Instance.Game.Map.Grid.GetElements(Player))
            {
                IResourceCollector collector = element as IResourceCollector;

                if (collector != null)
                {
                    collector.CollectResources();
                }
            }

            Finish();
        }
    }
}