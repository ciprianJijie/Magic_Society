using SimpleJSON;

namespace MS
{
    public class Model : ModelElement
    {
        public Model()
        {

        }

        public override void FromJSON(JSONNode json)
        {
        }

        public override JSONNode ToJSON()
        {
            JSONNode json = new JSONNode();

            return json;
        }
    }
}
