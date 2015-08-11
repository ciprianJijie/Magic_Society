using System;
using MS.Model.Kingdom;
using UnityEngine;

namespace MS.Views.Kingdom
{
    public class BuildingView : View<MS.Model.Kingdom.Building>
    {
        public GameObject TownHallPrefab;

        protected GameObject m_InstantiatedView;

        public override void UpdateView()
        {
            if (m_InstantiatedView != null)
            {
                Destroy(m_InstantiatedView.gameObject);
            }

            GameObject prefab;

            if (Model is MS.Model.Kingdom.TownHall)
            {
                prefab = TownHallPrefab;
            }
            else
            {
                prefab = null;
            }

            if (prefab != null)
            {
                m_InstantiatedView = Utils.Instantiate(prefab, this.transform, this.transform.position, this.transform.rotation);
            }
        }

        public override void UpdateView(Building element)
        {
            throw new NotImplementedException();
        }
    }
}