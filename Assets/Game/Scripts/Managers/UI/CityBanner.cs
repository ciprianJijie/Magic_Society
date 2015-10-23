using UnityEngine;
using UnityEngine.UI;
using MS.Controllers.UI.Heraldry;
using MS.Views.UI.Heraldry;

namespace MS.Managers.UI
{
    public class CityBanner : UIOverObject
    {
        public Text                 NameLabel;
        public Text                 PopulationLabel;
        public Text                 TurnsToGrowLabel;
        public Transform            ShieldHolder;
        [HideInInspector]
        public ShieldController     ShieldController;

        public void UpdateBanner(Model.City city)
        {
            int turnsToGrow;
            int foodToGrow;
            int foodPerTurn;

            NameLabel.text          =   city.RealName;
            PopulationLabel.text    =   city.Population.ToString();
            foodToGrow              =   city.CalculateFoodForNextPopulationUnit(city.Population);
            foodPerTurn             =   city.CollectFood().GetTotalAmount();
            turnsToGrow             =   Mathf.CeilToInt((float)(foodToGrow - city.Food) / (float)(foodPerTurn));
            TurnsToGrowLabel.text   =   turnsToGrow.ToString();

            if (city.ChiefHouse == null)
            {
                UnityEngine.Debug.LogError("City " + city + " is now owned by a house.");
            }
            else
            {
                ShieldController.Holder     =   ShieldHolder;
                var view                    =   ShieldController.CreateView(city.ChiefHouse.Shield) as ShieldView;

                view.UpdateView(city.ChiefHouse.Shield);
            }                

            
        }
    }
}
