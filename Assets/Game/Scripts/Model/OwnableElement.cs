using SimpleJSON;

namespace MS.Model
{
    public class OwnableElement : ModelElement
    {
        public Player Owner;

        public override void FromJSON(JSONNode json)
        {
            Owner = GameController.Instance.Game.Players.Find(json["owner"]);
        }

        public override JSONNode ToJSON()
        {
            JSONNode json = new JSONNode();

            json.Add("owner", Owner.Name);

            return json;
        }
    }
}

