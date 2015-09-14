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

        // Events
        public MS.Events.BuildingEvent OnBuildingButtonHover    =   MS.Events.DefaultAction;
        public MS.Events.Event OnBuildingButtonHoverEnds        =   MS.Events.DefaultAction;

        public void Show(Model.City city)
        {
            CityMenu.SetActive(true);

            foreach (Model.Kingdom.Building building in Game.Instance.Schemes)
            {
                if (city.Has(building) == false)
                {
                    var view = BuildingSchemeController.CreateView(building);
                    view.UpdateView(building);

                    var schemeView = view as Views.UI.BuildingSchemeView;
                    schemeView.OnBuildingHover      +=  OnBuildingButtonHoverEvent;
                    schemeView.OnBuildingHoverEnds  +=  OnBuildingButtonHoverEndsEvent;
                    schemeView.OnDestroyed          +=  OnSchemeViewDestroyed;
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

        protected void OnBuildingButtonHoverEvent(Model.Kingdom.Building building)
        {
            OnBuildingButtonHover(building);
        }

        protected void OnBuildingButtonHoverEndsEvent()
        {
            OnBuildingButtonHoverEnds();
        }

        protected void OnSchemeViewDestroyed(IUpdatableView schemeView)
        {
            var view = schemeView as Views.UI.BuildingSchemeView;

            view.OnBuildingHover -= OnBuildingButtonHoverEvent;
            view.OnBuildingHoverEnds -= OnBuildingButtonHoverEndsEvent;
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
