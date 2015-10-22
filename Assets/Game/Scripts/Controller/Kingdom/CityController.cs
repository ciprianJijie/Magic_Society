using System;
using System.Collections.Generic;
using MS.Model;
using MS.Model.Kingdom;
using UnityEngine;

namespace MS.Controllers.Kingdom
{
    public class CityController : Controller<CityView, Model.City>
    {
        public override void UpdateAllViews()
        {
            foreach (CityView view in m_Views)
            {
                view.UpdateView(view.Model);
                view.UpdateBanner(view.Model);
            }
        }
    }
}