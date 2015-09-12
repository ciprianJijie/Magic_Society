using MS.Model.Kingdom;
using UnityEngine;
using UnityEngine.UI;

namespace MS.Views.UI
{
    public class BuildingSchemeView : View<Model.Kingdom.Building>
    {
        public Text NameLabel;
        public Text DescriptionLabel;
        public Text GoldCostLabel;
        public Text ProductionCostLabel;
        public Image BackgroundImage;

        // Images for each Building

        public override void UpdateView(Building element)
        {
            BindTo(element);

            NameLabel.text = element.Name;
            DescriptionLabel.text = element.Description;
            GoldCostLabel.text = element.GoldCost.ToString();
            ProductionCostLabel.text = element.ProductionCost.ToString();

            // TODO: Assign background image
        }
    }
}
