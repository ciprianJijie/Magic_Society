using SimpleJSON;

namespace MS
{
    public abstract class Building : ModelElement
    {
        public override void FromJSON(JSONNode json)
        {
            Name = json["type"];
        }

        public override JSONNode ToJSON()
        {
            JSONNode json = new JSONNode();

            json["type"] = Name;

            return json;
        }

        public string Name;
    }
}
