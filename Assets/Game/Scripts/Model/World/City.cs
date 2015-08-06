using SimpleJSON;

namespace MS
namespace MS.Model
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
