using UnityEngine;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;

namespace MS.Model
{
    public class Player : ModelElement
    {
        public string   Name;
        public int      Gold;
        public int      Research;

        public Player()
        {
            Name = "Unnamed";
        }

        public Player(string name)
        {
            Name = name;
        }

        public virtual void Play<T>(T phase) where T: Phase
        {
            phase.Finish();
        }

        public override void FromJSON(JSONNode json)
        {
            Name        =   json["name"];
            Gold        =   json["gold"].AsInt;
            Research    =   json["research"].AsInt;
        }

        public override JSONNode ToJSON()
        {
            JSONClass root = new JSONClass();

            root.Add("name", Name);
            root.Add("gold", new JSONData(Gold));
            root.Add("research", new JSONData(Research));

            return root;
        }

        public static Player Create(string type)
        {
            Player player;

            switch (type)
            {
                case "AI":
                    player = new AIPlayer();
                    break;
                default:
                    player = new HumanPlayer();
                    break;
            }

            return player;
        }
    }
}
