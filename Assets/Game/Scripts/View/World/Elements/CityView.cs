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
            //GameObject prefab;

            //// TODO: Select prefab depending on population

            //prefab = VillagePrefab;

            //if (m_InstantedCity != null)
            //{
            //    Destroy(m_InstantedCity.gameObject);
            //}

            //if (m_InstancedUIBanner != null)
            //{
            //    Destroy(m_InstancedUIBanner.gameObject);
            //}

            //GameObject ui;

            //ui = GameObject.FindWithTag("UI");

            //m_InstantedCity                 =   Utils.Instantiate(prefab, this.transform, this.transform.position, this.transform.rotation);
            //m_InstancedUIBanner             =   Utils.Instantiate<Managers.UI.CityBanner>(UIBanner, ui.transform, this.transform.position, this.transform.rotation);
            //m_InstancedUIBanner.Target      =   this.gameObject.transform;
            //m_InstancedUIBanner.Label.text  =   Model.Name;
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

            if (m_InstancedUIBanner != null)
            {
                Destroy(m_InstancedUIBanner.gameObject);
            }

            GameObject ui;

            ui = GameObject.FindWithTag("UI");

            m_InstantedCity = Utils.Instantiate(prefab, this.transform, this.transform.position, this.transform.rotation);
            m_InstancedUIBanner = Utils.Instantiate<Managers.UI.CityBanner>(UIBanner, ui.transform, this.transform.position, this.transform.rotation);
            m_InstancedUIBanner.Target = this.gameObject.transform;
            m_InstancedUIBanner.Label.text = element.RealName;
        }
    }
}
