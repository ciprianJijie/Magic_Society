using UnityEngine;
using UnityEngine.UI;

namespace MS.Controllers.UI
{
    public class BuildingSchemeController : Controller<Views.UI.BuildingSchemeView, Model.Kingdom.Building>
    {
        // Events
        public MS.Events.BuildingEvent OnBuild = MS.Events.DefaultAction;

        public override IUpdatableView<MS.Model.Kingdom.Building> CreateView(MS.Model.Kingdom.Building modelElement, MS.Views.UI.BuildingSchemeView viewPrefab)
        {
            var view = base.CreateView(modelElement, viewPrefab);

            // Subscribe to events
            Views.UI.BuildingSchemeView schemeView;

            schemeView = view as Views.UI.BuildingSchemeView;
            schemeView.OnBuild += OnBuildEvent;

            return view;
        }

        protected void OnBuildEvent(Model.Kingdom.Building scheme)
        {
            UnityEngine.Debug.Log("Detected Build event on Scheme Controller");
            OnBuild(scheme);
        }
    }
}
