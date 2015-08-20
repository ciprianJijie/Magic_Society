using SimpleJSON;

namespace MS.Model
{
	public class OwnableMapElement : MapElement
	{
		public Player Owner;

        public override void FromJSON(JSONNode json)
        {
            base.FromJSON(json);

            if (json["owner"] != null)
            {
                Player player;

                player = GameController.Instance.Game.Players.Find(json["owner"]);

                if (player == null)
                {
                    UnityEngine.Debug.LogWarning("No player named " + json["owner"] + " was found. Assigning Neutral Player by default.");
                    player = GameController.Instance.Game.Players.Find("Neutral");
                }

                Owner = player;
            }
        }

        public override JSONNode ToJSON()
        {
            JSONNode json;

            json = base.ToJSON();

            json.Add("owner", Owner != null ? new JSONData(Owner.Name) : "None");

            return json;
        }
	}
}