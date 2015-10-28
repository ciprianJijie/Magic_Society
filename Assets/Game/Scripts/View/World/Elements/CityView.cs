using UnityEngine;
using MS.Model;
using MS.Controllers.UI.Heraldry;

namespace MS
{
    public class CityView : View<City>
    {
        public GameObject                   VillagePrefab;
        public GameObject                   TownPrefab;
        public GameObject                   CityPrefab;
        public GameObject                   MegapolisPrefab;

        public Managers.UI.CityBanner       UIBanner;
        [HideInInspector]
        public Transform                    BannerHolder;

        public ShieldController             ShieldController;

        protected GameObject                m_InstantedCity;
        protected Managers.UI.CityBanner    m_InstancedUIBanner;

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

            m_Model = element;

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
                m_InstancedUIBanner = Utils.Instantiate<Managers.UI.CityBanner>(UIBanner, BannerHolder, BannerHolder.position, BannerHolder.rotation);
                m_InstancedUIBanner.Target = this.gameObject.transform;
                m_InstancedUIBanner.ShieldController = ShieldController;
            }

            m_InstancedUIBanner.UpdateBanner(element);          
        }

        public void OnTurnsStarted()
        {
            UpdateView(Model);
        }

        protected void Start()
        {
            Game.Instance.Turns.OnFirstPlayerTurn += OnTurnsStarted;
        }
        
        protected void OnDestroy()
        {
            Game.Instance.Turns.OnFirstPlayerTurn -= OnTurnsStarted;
        }
    }
}
