using MS.Model;
using UnityEngine.UI;
using UnityEngine;

namespace MS.Views.UI
{
    public class ResourceAmountView : View<Model.ResourceAmount>
    {
        public Text AmountLabel;
        public Image Icon;
        public LocalizedText SourceNameLabel;

        public Sprite FoodIcon;
        public Sprite ProductionIcon;
        public Sprite GoldIcon;
        public Sprite ResearchIcon;

        public Color FoodColor;
        public Color ProductionColor;
        public Color GoldColor;
        public Color ResearchColor;

        public override void UpdateView(ResourceAmount element)
        {
            AmountLabel.text        =   element.Amount.ToString();
            AmountLabel.color       =   SelectColor(element.Resource);
            Icon.sprite             =   SelectSprite(element.Resource);
            
            if (element.Source is City)
            {
                SourceNameLabel.ID  =   "WORKERS";
            }
            else
            {
                SourceNameLabel.ID  =   element.Source.Name;
            }

            SourceNameLabel.UpdateText();
        }

        protected Color SelectColor(Resource resource)
        {
            if (resource is Food)
            {
                return FoodColor;
            }
            else if (resource is Production)
            {
                return ProductionColor;
            }
            else if (resource is Gold)
            {
                return GoldColor;
            }
            else if (resource is Research)
            {
                return ResearchColor;
            }

            return Color.white;
        }

        protected Sprite SelectSprite(Resource resource)
        {
            if (resource is Food)
            {
                return FoodIcon;
            }
            else if (resource is Production)
            {
                return ProductionIcon;
            }
            else if (resource is Gold)
            {
                return GoldIcon;
            }
            else if (resource is Research)
            {
                return ResearchIcon;
            }

            return null;
        }
    }
}
