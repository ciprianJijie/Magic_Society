
using SimpleJSON;

namespace MS.Model
{
    public abstract class Modifier : ModelElement
    {
        public int Value;

        public abstract int Apply(ModelElement element);

        public override void FromJSON(JSONNode json)
        {
            base.FromJSON(json);
            Value = json["value"].AsInt;
        }

        public override JSONNode ToJSON()
        {
            JSONNode root;

            root = base.ToJSON();

            root.Add("value", new JSONData(Value));

            return root;
        }

        public static Modifier Create(string type)
        {
            if (type == "ABILITY_MODIFIER")
            {
                return new AbilityModifier();
            }
            else if (type == "RELATIONSHIP_MODIFIER")
            {
                return new RelationshipModifier();
            }

            return null;
        }
    }
}
