using UnityEngine;
using UnityEngine.UI;

namespace MS.Controllers.UI
{
    public class CityController : MonoBehaviour
    {
        public GameObject               CityMenu;
        public BuildingSchemeController BuildingSchemeController;
        public BuildingController       BuildingController;
        public GameInputManager         InputManager;

        public void Show(Model.City city)
        {
            CityMenu.SetActive(true);

            foreach (Model.Kingdom.Building building in Game.Instance.Schemes)
            {
                if (city.Has(building) == false)
                {
                    var view = BuildingSchemeController.CreateView(building);
                    view.UpdateView(building);
                }
                else
                {
                    var view = BuildingController.CreateView(building);
                    view.UpdateView(building);
                }
                
            }
        }

        public void Hide()
        {
            CityMenu.SetActive(false);

            BuildingSchemeController.ClearViews();
            BuildingController.ClearViews();
        }

        protected void Start()
        {
            if (InputManager == null)
            {
                UnityEngine.Debug.LogWarning("Input is nos assigned for " + this);
            }
            InputManager.OnCitySelected += Show;
            InputManager.OnCityDeselected += Hide;

            Hide();
        }

        protected void OnDestroy()
        {
            InputManager.OnCitySelected -= Show;
            InputManager.OnCityDeselected -= Hide;
        }
    }
}
