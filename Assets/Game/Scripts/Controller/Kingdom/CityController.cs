using System;
using System.Collections.Generic;
using MS.Model.Kingdom;
using UnityEngine;

namespace MS.Controllers.Kingdom
{
    public class CityController : MonoBehaviour
    {
        public DistrictController   DistrictPrefab;
        public Transform            Holder;

        protected IList<DistrictController> m_Controllers;

        void Awake()
        {
            m_Controllers = new List<DistrictController>();
        }

        public DistrictController SpawnDistrict(CityDistrict district)
        {
            DistrictController controller;

            controller  =   Utils.Instantiate<DistrictController>(DistrictPrefab, Holder, Holder.position, Holder.rotation);          

            m_Controllers.Add(controller);

            return controller;
        }

        public void DemolishDistrict(CityDistrict district)
        {
            DistrictController toRemove;

            toRemove = null;

            foreach (DistrictController controller in m_Controllers)
            {
                if (controller.HasViewFor(district))
                {
                    toRemove = controller;
                    break;
                }
            }

            if (toRemove != null)
            {
                toRemove.DestroyView(district);
                m_Controllers.Remove(toRemove);
                Destroy(toRemove.gameObject);
            }            
        }

        public void UpdateAllControllers()
        {
            foreach (IViewCreator<CityDistrict> controller in m_Controllers)
            {
                controller.UpdateAllViews();
            }
        }
    }
}