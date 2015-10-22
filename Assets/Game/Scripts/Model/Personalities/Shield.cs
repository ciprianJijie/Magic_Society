using UnityEngine;

namespace MS.Model.Heraldry
{
    public class Shield : ModelElement, IRandomizable
    {
        public Field    Field;
        public Sprite   Emblem;
        public Color    EmblemColor;

        public Shield()
        {
            Field = new Field();
        }

        public void Randomize()
        {
            Field.Randomize();

            Emblem = Managers.Heraldry.HeraldryResourcesManager.Instance.GetRandomEmblem();

            do
            {
                EmblemColor = Managers.Heraldry.HeraldryResourcesManager.Instance.GetRandomColor();
            } while (Utils.Distance(EmblemColor, Field.PrimaryColor) < 150 || Utils.Distance(EmblemColor, Field.SecondaryColor) < 150);
            
        }
    }
}
