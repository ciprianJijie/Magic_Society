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
            if (m_InstancedUIBanner == null)
            {
                GameObject ui;

                ui = GameObject.FindWithTag("UI");

                m_InstancedUIBanner = Utils.Instantiate<Managers.UI.CityBanner>(UIBanner, ui.transform, this.transform.position, this.transform.rotation);
                m_InstancedUIBanner.Target = this.gameObject.transform;
            }

            m_InstancedUIBanner.UpdateBanner(element);
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
