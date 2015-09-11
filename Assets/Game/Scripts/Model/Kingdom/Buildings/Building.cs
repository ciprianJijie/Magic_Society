using SimpleJSON;

namespace MS.Model.Kingdom
{
    public abstract class Building : OwnableElement
    {
        public City     City;
        public int      GoldCost;
        public int      ProductionCost;

        public Building()
        {

        }

        public abstract void Use();
        public abstract void OnRecollection();
        public abstract void OnUpkeep(); 

        public override void FromJSON(JSONNode json)
        {
            base.FromJSON(json);

            GoldCost        =   json["gold_cost"].AsInt;
            ProductionCost  =   json["production_cost"].AsInt;

            if (json["city"] != null)
            {
                City = Game.Instance.Map.Grid.GetElement(Owner, json["city"]) as City;
            }
        }

        public override JSONNode ToJSON()
        {
            JSONNode json;

            json = base.ToJSON();

            json.Add("name", Name);
            json.Add("gold_cost", new JSONData(GoldCost));
            json.Add("production_cost", new JSONData(ProductionCost));
            
            if (City != null)
            {
                json.Add("city", City.Name);
            }

            return json;
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", Name, City.RealName);
        }

        public static class Factory
        {
            public static Building Create(string name)
            {
                if (name == "Town Hall")
                {
                    return new TownHall();
                }
                else
                {
                    throw new FactoryMethodWrongType("MS.Model.Kingdom.Building");
                }
            }
        }
    }
}

