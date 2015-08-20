using SimpleJSON;

namespace MS.Model
{
	public class AIPlayer : Player
	{
        public AIPlayer()
            : base("AI Player")
        {

        }

        public AIPlayer(string name)
            : base(name)
        {

        }

        public override JSONNode ToJSON()
        {
            JSONNode json;

            json = base.ToJSON();

            json.Add("type", new JSONData("AI"));

            return json;
        }
    }
}
