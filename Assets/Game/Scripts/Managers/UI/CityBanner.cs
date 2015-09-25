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

            if (city.Owner is Model.HumanPlayer && Game.Instance.Turns.CurrentTurn.Player is Model.HumanPlayer)
            {
                //UnityEngine.Debug.Log(string.Format("Food: {0} Grow at: {1} Generates: {2}", city.Food, foodToGrow, foodPerTurn - foodConsumption));
                UnityEngine.Debug.Log(string.Format("Stored: {0} Remaining: {1} Per Turn: {2} Turns: {3}", city.Food, foodToGrow - city.Food, foodPerTurn, turnsToGrow));
            }            
        }
    }
}
