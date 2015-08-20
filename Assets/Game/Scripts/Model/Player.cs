using SimpleJSON;

namespace MS.Model
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
            Name = json["name"];
        }

        public override JSONNode ToJSON()
        {
            JSONClass root = new JSONClass();

            root.Add("name", Name);

            return root;
        }

        public static Player Create(string type)
        {
            Player player;

            switch (type)
            {
                case "AI":
                    player = new AIPlayer();
                    break;
                default:
                    player = new HumanPlayer();
                    break;
            }

            return player;
        }
    }
}
