using SimpleJSON;

namespace MS
{
	public class City : OwnableMapElement
	{
        public override void FromJSON(JSONNode json)
        {
            base.FromJSON(json);
        }

        public override JSONNode ToJSON()
        {
            return base.ToJSON();
        }
	}
}
