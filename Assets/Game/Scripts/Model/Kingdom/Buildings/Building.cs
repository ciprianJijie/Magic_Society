using SimpleJSON;

namespace MS.Model.Kingdom
{
    public class Building : OwnableElement
    {
        public string Name;

        public Building()
        {

        }

        public override void FromJSON(JSONNode json)
        {
            base.FromJSON(json);
        }

        public override JSONNode ToJSON()
        {
            JSONNode json;

            json = base.ToJSON();

            return json;
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

