using System;
using SimpleJSON;
using UnityEngine;

namespace MS.Model.Heraldry
{
    public class Field : ModelElement, IRandomizable
    {
        public Sprite   PrimarySprite;
        public Sprite   SecondarySprite;
        public Color    PrimaryColor;
        public Color    SecondaryColor;

        public Field()
        {
            Name = "HERALDRY_SHIELD_FIELD";
        }

        public override void FromJSON(JSONNode json)
        {
            base.FromJSON(json);
            
            PrimarySprite       =   Managers.Heraldry.HeraldryResourcesManager.Instance.FindPrimaryField(json["primary"]);
            SecondarySprite     =   Managers.Heraldry.HeraldryResourcesManager.Instance.FindSecondaryField(json["secondary"]);
        }

        public override JSONNode ToJSON()
        {
            JSONNode root;

            root = base.ToJSON();
            root.Add("primary", PrimarySprite.name);
            root.Add("secondary", SecondarySprite.name);

            return root;
        }

        public void Randomize()
        {
            int colorDistance;
            int steps;

            Managers.Heraldry.HeraldryResourcesManager.Instance.GetRandomField(out PrimarySprite, out SecondarySprite);

            steps = 0;

            do
            {
                PrimaryColor    =   Managers.Heraldry.HeraldryResourcesManager.Instance.GetRandomColor();
                SecondaryColor  =   Managers.Heraldry.HeraldryResourcesManager.Instance.GetRandomColor();
                colorDistance   =   Utils.Distance(PrimaryColor, SecondaryColor);
                steps++;

            } while (colorDistance < 150);
            
        }
    }
}
