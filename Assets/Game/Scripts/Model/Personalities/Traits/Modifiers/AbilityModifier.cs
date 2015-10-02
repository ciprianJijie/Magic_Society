
using System;
using SimpleJSON;

namespace MS.Model
{
    public class AbilityModifier : Modifier
    {
        public Ability.EType AbilityType;

        public AbilityModifier()
        {
            Name = "ABILITY_MODIFIER";
        }

        public override int Apply(ModelElement element)
        {
            Ability ability;

            ability = element as Ability;

            if (ability != null)
            {
                if (AbilityType == ability.Type)
                {
                    return Value;
                }
            }

            return 0;
        }

        public override void FromJSON(JSONNode json)
        {
            base.FromJSON(json);
            AbilityType = EnumUtils.ParseEnum<Ability.EType>(json["ability"]);
        }

        public override JSONNode ToJSON()
        {
            JSONNode root = base.ToJSON();

            root.Add("ability", AbilityType.ToString());

            return root;
        }

        public override string ToString()
        {
            return string.Format("{0} for {1} by {2}", Name, AbilityType, Value);
        }
    }
}
