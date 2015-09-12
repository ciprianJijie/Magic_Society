using UnityEngine;
using UnityEngine.UI;

namespace MS.Controllers.UI
{
    public class CityController : MonoBehaviour
    {
        public GameObject           CityMenu;
        public BuildingSchemeController   BuildingController;
        public GameInputManager     InputManager;

        public void Show(Model.City city)
        {
            CityMenu.SetActive(true);

            foreach (Model.Kingdom.Building building in Game.Instance.Schemes)
            {
                var view = BuildingController.CreateView(building);
                view.UpdateView(building);
            }
        }

        public void Hide()
        {
            CityMenu.SetActive(false);

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
