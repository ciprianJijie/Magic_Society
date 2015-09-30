using SimpleJSON;
using UnityEngine;

namespace MS.Model
{
    public class Ability : ModelElement
    {
        public int Score;

        public int Modifier
        {
            get
            {
                return Mathf.FloorToInt(((float)(Score - 10)) / 2f);
            }
        }

        public Ability(string name, int score)
        {
            Name = name;
            Score = score;
        }

        public override void FromJSON(JSONNode json)
        {
            base.FromJSON(json);
            Score = json["score"].AsInt;
        }

        public override JSONNode ToJSON()
        {
            JSONNode root;

            root = base.ToJSON();
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
