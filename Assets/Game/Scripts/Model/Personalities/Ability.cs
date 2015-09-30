using SimpleJSON;
using UnityEngine;

namespace MS.Model
{
    public class Ability : ModelElement
    {
        public enum EType
        {
            ABILITY_STRENGTH,
            ABILITY_DEXTERITY,
            ABILITY_CONSTITUTION,
            ABILITY_INTELLIGENCE,
            ABILITY_WISDOM,
            ABILITY_CHARISMA
        }

        public EType    Type;
        public int      Score;

        public int Modifier
        {
            get
            {
                return Mathf.FloorToInt(((float)(Score - 10)) / 2f);
            }
        }

        public Ability(EType type, int score)
        {
            Name    =   type.ToString();
            Score   =   score;
            Type    =   type;
        }

        public override void FromJSON(JSONNode json)
        {
            Type    =   EnumUtils.ParseEnum<EType>(json["name"]);
            Score   =   json["score"].AsInt;
        }

        public override JSONNode ToJSON()
        {
            JSONClass root;

            root = new JSONClass();

            root.Add("name", Type.ToString());
            root.Add("score", new JSONData(Score));

            return root;
        }

        public override string ToString()
        {
            if (Modifier >= 0)
            {
                return string.Format("{0}(<color=green>+{1}</color>)", Score, Modifier);
            }
            return string.Format("{0}(<color=red>{1}</color>)", Score, Modifier);
        }
    }
}
