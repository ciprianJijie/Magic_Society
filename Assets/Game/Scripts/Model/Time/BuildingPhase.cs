using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MS.Model
{
    public class BuildingPhase : Phase
    {
        public BuildingPhase()
        {
            Name = "BUILDING_PHASE";
        }

        public override void Execute()
        {
            foreach (City city in Game.Instance.Map.Grid.GetElements(this.Player))
            {
                if (city != null)
                {
                    city.BuildCompletedBuilding();
                }
            }

            Finish();
        }
    }
}
