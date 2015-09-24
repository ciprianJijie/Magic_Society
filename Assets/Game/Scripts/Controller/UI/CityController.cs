﻿using UnityEngine;
using UnityEngine.UI;

namespace MS.Controllers.UI
{
    public class CityController : MonoBehaviour
    {
        public GameObject                           CityMenu;
        public BuildingSchemeController             BuildingSchemeController;
        public BuildingController                   BuildingController;
        public ResourceAmountController             ResourceAmountController;
        public BuildingQueueController              BuildingQueueController;
        public Managers.UI.BuildingPanelManager     BuildingPanelManager;
        public GameInputManager                     InputManager;
        public RepeatableIcon                       IdleWorkersArea;
        public RepeatableIcon                       FoodWorkersArea;
        public RepeatableIcon                       ProductionWorkersArea;
        public RepeatableIcon                       GoldWorkersArea;
        public RepeatableIcon                       ResearchWorkersArea;
        public Text                                 FoodAmountLabel;
        public Text                                 ProductionAmountLabel;
        public Text                                 GoldAmountLabel;
        public Text                                 ResearchAmountLabel;


        // Events
        public MS.Events.BuildingEvent OnBuildingButtonHover    =   MS.Events.DefaultAction;
        public MS.Events.Event OnBuildingButtonHoverEnds        =   MS.Events.DefaultAction;

        protected Model.ResourceAdvancedAmount m_FoodEstimatedRecollection;
        protected Model.ResourceAdvancedAmount m_ProductionEstimatedRecollection;
        protected Model.ResourceAdvancedAmount m_GoldEstimatedCollection;
        protected Model.ResourceAdvancedAmount m_ResearchEstimatedCollection;

        public void Show(Model.City city)
        {
            CityMenu.SetActive(true);

            UpdateBuildingSchemesArea(city);
            UpdateBuildingsList(city);
            UpdateRecollectionArea(city);

            BuildingQueueController.Show(city.BuildingQueue);
        }

        public void Hide()
        {
            CityMenu.SetActive(false);

            BuildingSchemeController.ClearViews();
            BuildingController.ClearViews();
            BuildingQueueController.Hide();

            ResourceAmountController.Hide();
        }

        public void UpdateRecollectionArea(Model.City city)
        {
            IdleWorkersArea.UpdateIcons(city.AvailableWorkers);
            FoodWorkersArea.UpdateIcons(city.FoodWorkers);
            ProductionWorkersArea.UpdateIcons(city.ProductionWorkers);
            GoldWorkersArea.UpdateIcons(city.GoldWorkers);
            ResearchWorkersArea.UpdateIcons(city.ResearchWorkers);
            
            // Calculate amount recollected
            m_FoodEstimatedRecollection         =   city.CollectFood();
            m_ProductionEstimatedRecollection   =   city.CollectProduction();
            m_GoldEstimatedCollection           =   city.CollectGold();
            m_ResearchEstimatedCollection       =   city.CollectResearch();

            FoodAmountLabel.text        =   m_FoodEstimatedRecollection.GetTotalAmount().ToString();
            ProductionAmountLabel.text  =   m_ProductionEstimatedRecollection.GetTotalAmount().ToString();
            GoldAmountLabel.text        =   m_GoldEstimatedCollection.GetTotalAmount().ToString();
            ResearchAmountLabel.text    =   m_ResearchEstimatedCollection.GetTotalAmount().ToString();
        }

        public void UpdateBuildingSchemesArea(Model.City city)
        {
            foreach (Model.Kingdom.Building building in Game.Instance.Schemes)
            {
                BuildingSchemeController.DestroyView(building);

                if (city.Has(building) == false &&
                    city.BuildingQueue.IsProducing(building) == false)
                {
                    var view = BuildingSchemeController.CreateView(building);
                    view.UpdateView(building);

                    var schemeView = view as Views.UI.BuildingSchemeView;
                    schemeView.OnBuildingHover += OnBuildingButtonHoverEvent;
                    schemeView.OnBuildingHoverEnds += OnBuildingButtonHoverEndsEvent;
                    schemeView.OnDestroyed += OnSchemeViewDestroyed;
                }
            }
            ResourceAmountController.Hide();
        }

        public void UpdateBuildingsList(Model.City city)
        {
            foreach (Model.Kingdom.Building building in Game.Instance.Schemes)
            {
                if (city.Has(building))
                {
                    BuildingController.DestroyView(building);
                    var view = BuildingController.CreateView(building);
                    view.UpdateView(building);
                }
            }
            ResourceAmountController.Hide();
        }

        public void AddWorkersToFood(int amount)
        {
            GameController.Instance.SelectedCity.FoodWorkers += CalculateWorkersToAssign(amount, GameController.Instance.SelectedCity.AvailableWorkers);
            UpdateRecollectionArea(GameController.Instance.SelectedCity);
        }

        public void RemoveWorkersFromFood(int amount)
        {
            if (GameController.Instance.SelectedCity.FoodWorkers > 0)
            {
                GameController.Instance.SelectedCity.FoodWorkers -= amount;
                UpdateRecollectionArea(GameController.Instance.SelectedCity);
            }            
        }

        public void AddWorkersToProduction(int amount)
        {
            GameController.Instance.SelectedCity.ProductionWorkers += CalculateWorkersToAssign(amount, GameController.Instance.SelectedCity.AvailableWorkers);
            UpdateRecollectionArea(GameController.Instance.SelectedCity);
            UpdateBuildingSchemesArea(GameController.Instance.SelectedCity);
            BuildingQueueController.SingleItemController.UpdateAllViews();
        }

        public void RemoveWorkersFromProduction(int amount)
        {
            if (GameController.Instance.SelectedCity.ProductionWorkers > 0)
            {
                GameController.Instance.SelectedCity.ProductionWorkers -= amount;
                UpdateRecollectionArea(GameController.Instance.SelectedCity);
                UpdateBuildingSchemesArea(GameController.Instance.SelectedCity);
                BuildingQueueController.SingleItemController.UpdateAllViews();
            }
        }

        public void AddWorkersToGold(int amount)
        {
            GameController.Instance.SelectedCity.GoldWorkers += CalculateWorkersToAssign(amount, GameController.Instance.SelectedCity.AvailableWorkers);
            UpdateRecollectionArea(GameController.Instance.SelectedCity);
        }

        public void RemoveWorkersFromGold(int amount)
        {
            if (GameController.Instance.SelectedCity.GoldWorkers > 0)
            {
                GameController.Instance.SelectedCity.GoldWorkers -= amount;
                UpdateRecollectionArea(GameController.Instance.SelectedCity);
            }
        }

        public void AddWorkersToResearch(int amount)
        {
            GameController.Instance.SelectedCity.ResearchWorkers += CalculateWorkersToAssign(amount, GameController.Instance.SelectedCity.AvailableWorkers);
            UpdateRecollectionArea(GameController.Instance.SelectedCity);
        }

        public void RemoveWorkersFromResearch(int amount)
        {
            if (GameController.Instance.SelectedCity.ResearchWorkers > 0)
            {
                GameController.Instance.SelectedCity.ResearchWorkers -= amount;
                UpdateRecollectionArea(GameController.Instance.SelectedCity);
            }
        }

        protected int CalculateWorkersToAssign(int amount, int availableWorkers)
        {
            return amount <= availableWorkers ? amount : availableWorkers;
        }

        // UI Events
        public void OnFoodAmountHover()
        {
            ResourceAmountController.Show(m_FoodEstimatedRecollection);
        }

        public void OnProductionAmountHover()
        {
            ResourceAmountController.Show(m_ProductionEstimatedRecollection);
        }

        public void OnGoldAmountHover()
        {
            ResourceAmountController.Show(m_GoldEstimatedCollection);
        }

        public void OnResearchAmountHover()
        {
            ResourceAmountController.Show(m_ResearchEstimatedCollection);
        }

        public void OnResourceAmountHoverEnds()
        {
            ResourceAmountController.Hide();
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

            //Hide();
            CityMenu.SetActive(false);
        }

        protected void OnDestroy()
        {
            InputManager.OnCitySelected -= Show;
            InputManager.OnCityDeselected -= Hide;
        }
    }
}
