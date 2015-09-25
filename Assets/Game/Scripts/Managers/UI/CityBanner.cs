using UnityEngine;
using UnityEngine.UI;

namespace MS.Managers.UI
{
    public class CityBanner : UIOverObject
    {
        public Text         NameLabel;
        public ProgressBar  PopulationBar;

        public Text         PopulationLabel;
        public Text         TurnsToGrowLabel;

        public void UpdateBanner(Model.City city)
        {
            int turnsToGrow;
            int foodToGrow;
            int min;
            int max;

            NameLabel.text          =   city.RealName;
            PopulationLabel.text    =   city.Population.ToString();
            foodToGrow              =   city.CalculateFoodForNextPopulationUnit(city.Population);
            turnsToGrow             =   Mathf.CeilToInt((float)foodToGrow / (float)city.CollectFood().GetTotalAmount());
            TurnsToGrowLabel.text   =   turnsToGrow.ToString();
            min = 0;
            max = foodToGrow;

            UnityEngine.Debug.Log("Updating progress bar [" + min + "->" + max + "] value: " + city.Food);

            PopulationBar.MinValue = min;
            PopulationBar.MaxValue = max;
            PopulationBar.SetValue(city.Food);
            //PopulationBar.SetPercentage((float)city.Food / (float)foodToGrow);
        }
    }
}
