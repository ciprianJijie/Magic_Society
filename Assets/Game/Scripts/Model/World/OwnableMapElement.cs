using SimpleJSON;

namespace MS
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
                    throw new System.NullReferenceException("No player named " + json["owner"] + " found in the current game.");
                }

                Owner = player;
            }
        }

        public override JSONNode ToJSON()
        {
            JSONNode json;

            json = base.ToJSON();

            json.Add("Owner", new JSONData(Owner.Name));

            return json;
        }
	}
}