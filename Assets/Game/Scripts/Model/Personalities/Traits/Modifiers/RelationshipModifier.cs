
using System;
using SimpleJSON;

namespace MS.Model
{
    public class RelationshipModifier : Modifier
    {
        public string TraitName;

        public RelationshipModifier()
        {
            Name = "RELATIONSHIP_MODIFIER";
        }

        public override int Apply(ModelElement element)
        {
            Relationship relation;

            relation = element as Relationship;

            if (relation != null)
            {
                if (relation.Target.Is(TraitName))
                {
                    return Value;
                }
            }

            return 0;
        }

        public override void FromJSON(JSONNode json)
        {
            base.FromJSON(json);
            TraitName = json["trait_name"];
        }

        public override JSONNode ToJSON()
        {
            JSONNode root = base.ToJSON();

            root.Add("trait_name", TraitName);

            return root;
        }

        public override string ToString()
        {
            return string.Format("{0} for {1} by {2}", Name, TraitName, Value);
        }
    }
}
