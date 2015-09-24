
using System;
using SimpleJSON;

namespace MS.Model.Kingdom
{
    public class BuildingQueueItem : ModelElement
    {
        public int          Production;
        public Building     Building;

        // Events
        public Events.BuildingQueueItemEvent OnFinished = Events.DefaultAction;

        public int ProductionUntilCompletion
        {
            get
            {
                return Building.ProductionCost - Production;
            }
        }

        public int RemainingProduction
        {
            get
            {
                if (Production > Building.ProductionCost)
                {
                    return Production - Building.ProductionCost;
                }
                else
                {
                    return 0;
                }
            }
        }

        public BuildingQueueItem()
        {

        }

        public BuildingQueueItem(int production, Building building)
        {
            Production  =   production;
            Building    =   building;
        }

        public void Produce(int amount)
        {
            Production += amount;

            if (Production >= Building.ProductionCost)
            {
                OnFinished(this);
            }
        }

        public override void FromJSON(JSONNode json)
        {
            Building    =   Building.Factory.Create(json["building"]);
            Production  =   json["production"].AsInt;
        }

        public override JSONNode ToJSON()
        {
            JSONClass root;

            root = new JSONClass();

            root.Add("building", Building.Name);
            root.Add("production", new JSONData(Production));

            return root;
        }

        public override string ToString()
        {
            return string.Format("{0} with {1} Production units", Building.Name, Production);
        }
    }
}
