using UnityEngine;
using MS.Model;

namespace MS.Controllers.Kingdom
{
    public class CityController : Controller<CityView, Model.City>
    {
        public UI.Heraldry.ShieldController ShieldController;
        public Transform                    BannersHolder;

        public override IUpdatableView<City> CreateView(City modelElement, CityView viewPrefab)
        {
            CityView cityView;
            var view = base.CreateView(modelElement, viewPrefab);

            cityView                    =   (view as CityView);
            cityView.ShieldController   =   ShieldController;
            cityView.BannerHolder       =   BannersHolder;

            return view;
        }

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