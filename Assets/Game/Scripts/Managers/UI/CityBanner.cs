using UnityEngine;
using UnityEngine.UI;

namespace MS.Managers.UI
{
    public class CityBanner : UIOverObject
    {
        public Text         NameLabel;
        public Text         PopulationLabel;
        public Text         TurnsToGrowLabel;

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
        }
    }
}
