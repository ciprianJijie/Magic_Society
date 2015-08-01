using SimpleJSON;

namespace MS
{    
    public class Player : ModelElement
    {
        public string Name;

        public Player()
        {
            Name = "Unnamed";
        }

        public Player(string name)
        {
            Name = name;
        }

        public override void FromJSON(JSONNode json)
        {

        }

        public override JSONNode ToJSON()
        {
            JSONNode json;

            json = new JSONNode();

            return json;
        }
    }
}
