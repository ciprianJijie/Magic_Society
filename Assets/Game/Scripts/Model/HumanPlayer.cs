using SimpleJSON;

namespace MS.Model
{
	public class HumanPlayer : Player
	{
        public HumanPlayer()
            : base("Human Player")
        {

        }

        public HumanPlayer(string name)
            : base(name)
        {

        }

        public override JSONNode ToJSON()
        {
            JSONNode json;

            json = base.ToJSON();

            json.Add("type", new JSONData("Human"));

            return json;
        }
    }
}