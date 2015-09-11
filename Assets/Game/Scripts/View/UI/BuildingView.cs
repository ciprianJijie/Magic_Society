using MS.Model.Kingdom;
using UnityEngine;
using UnityEngine.UI;

namespace MS.Views.UI
{
    public class BuildingView : View<Model.Kingdom.Building>
    {
        public Text NameLabel;
        public Text GoldCostLabel;
        public Text ProductionCostLabel;
        public Image BackgroundImage;

        public override void UpdateView(Building element)
        {
            BindTo(element);

            NameLabel.text = element.Name;
            GoldCostLabel.text = element.GoldCost.ToString();
            ProductionCostLabel.text = element.ProductionCost.ToString();

            // TODO: Assign background image
        }
    }
}
