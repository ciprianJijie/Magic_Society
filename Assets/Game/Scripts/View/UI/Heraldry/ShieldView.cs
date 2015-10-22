using UnityEngine;
using UnityEngine.UI;
using MS.Model.Heraldry;

namespace MS.Views.UI.Heraldry
{
    public class ShieldView : View<Model.Heraldry.Shield>
    {
        public Image PrimaryFieldRenderer;
        public Image SecondaryFieldRenderer;
        public Image EmblemRenderer;

        public override void UpdateView(Shield element)
        {
            PrimaryFieldRenderer.sprite     =   element.Field.PrimarySprite;
            SecondaryFieldRenderer.sprite   =   element.Field.SecondarySprite;
            PrimaryFieldRenderer.color      =   element.Field.PrimaryColor;
            SecondaryFieldRenderer.color    =   element.Field.SecondaryColor;
            EmblemRenderer.sprite           =   element.Emblem;
            EmblemRenderer.color            =   element.EmblemColor;
        }
    }
}
