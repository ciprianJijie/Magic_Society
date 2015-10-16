using SimpleJSON;

namespace MS.Model
{
    public class OwnableElement : ModelElement
    {
        public Player Owner;

        public override void FromJSON(JSONNode json)
        {
            base.FromJSON(json);
            Owner = Managers.GameManager.Instance.Game.Players.Find(json["owner"]);
        }

        public override JSONNode ToJSON()
        {
            JSONNode json = base.ToJSON();

            json.Add("owner", Owner.Name);

            return json;
        }
    }
}

