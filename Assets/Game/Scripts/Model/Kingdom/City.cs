using SimpleJSON;
using System.Collections.Generic;

namespace MS
{
    public class City : Building
    {
        public City()
        {
            Name        =   "No name";

            Citizens    =   0;
            CitizensMax =   0;

            Food        =   0;
            Wood        =   0;
            Iron        =   0;
            Stone       =   0;

            FoodMax     =   0;
            WoodMax     =   0;
            IronMax     =   0;
            StoneMax    =   0;

            m_Buildings = new List<Building>();
        }

        public override void FromJSON(JSONNode json)
        {
            Name        =   json["name"];

            Citizens    =   json["citizens"]["amount"].AsInt;
            CitizensMax =   json["citizens"]["max"].AsInt;

            Food        =   json["resources"]["food"]["amount"].AsInt;
            FoodMax     =   json["resources"]["food"]["max"].AsInt;
            Wood        =   json["resources"]["wood"]["amount"].AsInt;
            WoodMax     =   json["resources"]["wood"]["max"].AsInt;
            Iron        =   json["resources"]["iron"]["amount"].AsInt;
            IronMax     =   json["resources"]["iron"]["max"].AsInt;
            Stone       =   json["resources"]["stone"]["amount"].AsInt;
            StoneMax    =   json["resources"]["stone"]["max"].AsInt;

            foreach (JSONNode buildingNode in json["buildings"].AsArray)
            {
                Building building = Factory.Building.Create(buildingNode["type"]);

                m_Buildings.Add(building);
            }
        }

        public override JSONNode ToJSON()
        {
            JSONNode json = new JSONNode();

            json["name"]                            =   Name;

            json["citizens"]["amount"]              =   Citizens.ToString();
            json["citizens"]["max"]                 =   CitizensMax.ToString();

            json["resources"]["food"]["amount"]     =   Food.ToString();
            json["resources"]["food"]["max"]        =   FoodMax.ToString();
            json["resources"]["wood"]["amount"]     =   Wood.ToString();
            json["resources"]["wood"]["max"]        =   WoodMax.ToString();
            json["resources"]["iron"]["amount"]     =   Iron.ToString();
            json["resources"]["iron"]["max"]        =   IronMax.ToString();
            json["resources"]["stone"]["amount"]    =   Stone.ToString();
            json["resources"]["stone"]["max"]       =   StoneMax.ToString();

            return json;
        }

        public string   Name;

        public int      Citizens;
        public int      Food;
        public int      Wood;
        public int      Iron;
        public int      Stone;

        protected int   CitizensMax;
        protected int   FoodMax;
        protected int   WoodMax;
        protected int   IronMax;
        protected int   StoneMax;

        protected List<Building> m_Buildings;
    }
}
