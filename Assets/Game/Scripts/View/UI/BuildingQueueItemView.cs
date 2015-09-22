using System;
using UnityEngine;
using UnityEngine.UI;
using MS.Managers.UI;

namespace MS.Views.UI
{
    public class BuildingQueueItemView : View<Model.Kingdom.BuildingQueueItem>
    {
        public Image            Image;
        public LocalizedText    NameLabel;
        public ProgressBar      ProgressBar;
        public Text             TurnsLeftLabel;

        public Sprite           AquaductImage;
        public Sprite           BarracksImage;
        public Sprite           FarmImage;
        public Sprite           TownHallImage;

        public override void UpdateView(MS.Model.Kingdom.BuildingQueueItem element)
        {
            float   percentage;
            int     turnsLeft;

            Image.sprite            =   SelectSprite(element.Building);
            NameLabel.ID            =   element.Building.Name; 
            percentage              =   element.Production / element.Building.ProductionCost;
            turnsLeft               =   Mathf.RoundToInt(element.ProductionUntilCompletion / GameController.Instance.SelectedCity.CollectProduction().GetTotalAmount());
            ProgressBar.MinValue    =   0;
            ProgressBar.MaxValue    =   element.Building.ProductionCost;
            TurnsLeftLabel.text     =   turnsLeft.ToString();

            ProgressBar.SetPercentage(percentage);
            NameLabel.UpdateText();
        }

        protected Sprite SelectSprite(Model.Kingdom.Building building)
        {
            if (building is Model.Kingdom.Aquaduct)
            {
                return AquaductImage;
            }
            else if (building is Model.Kingdom.Barracks)
            {
                return BarracksImage;
            }
            else if (building is Model.Kingdom.Farm)
            {
                return FarmImage;
            }
            else if (building is Model.Kingdom.TownHall)
            {
                return TownHallImage;
            }

            return null;
        }
    }
}

