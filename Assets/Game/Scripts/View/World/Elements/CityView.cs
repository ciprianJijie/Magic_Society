using UnityEngine;
using MS.Model;
using System;

namespace MS
{
    public class CityView : View<City>
    {
        public GameObject                   VillagePrefab;
        public GameObject                   TownPrefab;
        public GameObject                   CityPrefab;
        public GameObject                   MegapolisPrefab;

        public Managers.UI.CityBanner       UIBanner;

        protected GameObject                m_InstantedCity;
        protected Managers.UI.CityBanner    m_InstancedUIBanner;

        public override void UpdateView()
        {
            UpdateBanner(Model);
        }

        public override void UpdateView(City element)
        {
            GameObject prefab;

            // TODO: Select prefab depending on population

            prefab = VillagePrefab;

            if (m_InstantedCity != null)
            {
                Destroy(m_InstantedCity.gameObject);
            }
            
            m_InstantedCity = Utils.Instantiate(prefab, this.transform, this.transform.position, this.transform.rotation);
            
            UpdateBanner(element);            
        }

        public void UpdateBanner()
        {
            UpdateBanner(Model);
        }

        public void UpdateBanner(City element)
        {
            if (m_InstancedUIBanner != null)
            {
                Destroy(m_InstancedUIBanner.gameObject);
            }

            GameObject ui;

            ui = GameObject.FindWithTag("UI");

            if (ui != null)
            {
                m_InstancedUIBanner = Utils.Instantiate<Managers.UI.CityBanner>(UIBanner, ui.transform, this.transform.position, this.transform.rotation);
                m_InstancedUIBanner.Target = this.gameObject.transform;
                m_InstancedUIBanner.Label.text = element.RealName;

                if (GameController.Instance.Game.Turns.CurrentTurn.Player == element.Owner)
                {
                    m_InstancedUIBanner.ShowPopulationBar("" + element.Population);
                    m_InstancedUIBanner.ShowBuildingBar("Building...");

                    m_InstancedUIBanner.PopulationBar.MinValue = 0f;
                    m_InstancedUIBanner.PopulationBar.MaxValue = element.CalculateFoodForNextPopulationUnit(element.Population);
                    m_InstancedUIBanner.PopulationBar.SetValue(element.Food);
                }
                else
                {
                    m_InstancedUIBanner.HidePopulationBar();
                    m_InstancedUIBanner.HideBuildingBar();
                }
            }
        }

        public void OnMainPhaseStarted(Player player)
        {
            if (player != Model.Owner)
            {
                return;
            }

            UpdateView();
        }

        protected void Start()
        {
            GameController.Instance.Game.Turns.OnMainPhase += OnMainPhaseStarted;
        }
        
        protected void OnDestroy()
        {
            if (GameController.Instance != null)
            {
                GameController.Instance.Game.Turns.OnMainPhase -= OnMainPhaseStarted;
            }            
        }
    }
}
