using UnityEngine.UI;

namespace MS.Managers.UI
{
    public class CityBanner : UIOverObject
    {
        public Text         Label;
        public ProgressBar  PopulationBar;
        public ProgressBar  BuildingBar;

        public Text         PopulationLabel;
        public Text         BuildingLabel;

        public void ShowPopulationBar(string text)
        {
            PopulationBar.gameObject.SetActive(true);

            PopulationLabel.text = text;
        }

        public void HidePopulationBar()
        {
            PopulationBar.gameObject.SetActive(false);
        }

        public void ShowBuildingBar(string text)
        {
            BuildingBar.gameObject.SetActive(true);

            BuildingLabel.text = text;
        }

        public void HideBuildingBar()
        {
            BuildingBar.gameObject.SetActive(false);
        }
    }
}
