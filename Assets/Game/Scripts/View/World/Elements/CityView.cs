using UnityEngine;

namespace MS
{
    public class CityView : View<City>
    {
        public GameObject VillagePrefab;
        public GameObject TownPrefab;
        public GameObject CityPrefab;
        public GameObject MegapolisPrefab;

        protected GameObject m_InstantedCity;

        public override void UpdateView()
        {
            GameObject prefab;

            // TODO: Select prefab depending on population

            prefab = VillagePrefab;

            if (m_InstantedCity != null)
            {
                Destroy(m_InstantedCity.gameObject);
            }

            m_InstantedCity = Utils.Instantiate(prefab, this.transform, this.transform.position, this.transform.rotation);
        }
    }
}
