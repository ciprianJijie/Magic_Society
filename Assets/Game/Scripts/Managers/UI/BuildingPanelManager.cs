using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace MS.Managers.UI
{
    public class BuildingPanelManager : MonoBehaviour
    {
        public Controllers.UI.CityController    CityController;
        public GameObject                       Panel;
        public LocalizedText                    NameLabel;
        public LocalizedText                    DescriptionLabel;
        public Text                             CostLabel;
        public GameObject                       CostArea;
        public Text                             GoldCostLabel;
        public Text                             ProductionCostLabel;
        public LocalizedText                    PersonalitiesLabel;
        public MS.Controllers.UI.RepeatableIcon PersonalitiesArea;

        protected RectTransform                 m_RectTransform;
       
        public void UpdatePanel(Model.Kingdom.Building building)
        {
            NameLabel.ID = building.Name;
            DescriptionLabel.ID = building.Description;

            if (building.City == null)
            {
                CostLabel.gameObject.SetActive(true);
                CostArea.gameObject.SetActive(true);

                GoldCostLabel.text = building.GoldCost.ToString();
                ProductionCostLabel.text = building.ProductionCost.ToString();
            }
            else
            {
                CostLabel.gameObject.SetActive(false);
                CostArea.gameObject.SetActive(false);
            }

            NameLabel.UpdateText();
            DescriptionLabel.UpdateText();

            // TODO: Show Personalities area and info
            PersonalitiesLabel.gameObject.SetActive(false);
            PersonalitiesArea.gameObject.SetActive(false);

            // Set Position
            Vector2 mousePosition;
            Vector2 panelPosition;

            mousePosition = Input.mousePosition;
            panelPosition = m_RectTransform.anchoredPosition;
            panelPosition.y = mousePosition.y;

            m_RectTransform.anchoredPosition = panelPosition;

            Panel.SetActive(true);
        }

        public void Show(Model.Kingdom.Building building)
        {
            UpdatePanel(building);
        }

        public void Hide()
        {
            Panel.SetActive(false);
        }

        protected void Start()
        {
            CityController.OnBuildingButtonHover        +=  UpdatePanel;
            CityController.OnBuildingButtonHoverEnds    +=  Hide;

            m_RectTransform = this.gameObject.GetComponent<RectTransform>();

            Hide();
        }

        protected void OnDestroy()
        {
            CityController.OnBuildingButtonHover        -=  UpdatePanel;
            CityController.OnBuildingButtonHoverEnds    -=  Hide;
        }
    }
}
