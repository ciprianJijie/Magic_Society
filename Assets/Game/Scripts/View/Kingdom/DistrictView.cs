using System;
using MS.Model.Kingdom;
using UnityEngine;

namespace MS.Views.Kingdom
{
    public class DistrictView : View<MS.Model.Kingdom.CityDistrict>
    {
        public Transform[] BuildingsPositions;

        public override void UpdateView()
        {
            throw new NotImplementedException();
        }

        public override void UpdateView(CityDistrict element)
        {
            throw new NotImplementedException();
        }
    }
}