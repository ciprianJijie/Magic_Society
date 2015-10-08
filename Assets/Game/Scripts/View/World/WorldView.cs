using MS.Model.World;
using UnityEngine;

namespace MS.Views.World
{
    public class WorldView : View<Model.World.World>
    {
        public Controllers.World.Map.RegionController RegionController;
        
        public override void UpdateView(Model.World.World element)
        {
            RegionController.DestroyViewsNotIn(element);

            foreach (Region region in element)
            {
                Map.RegionView  view;

                view = RegionController.FindView(region);

                if (view == null)
                {
                    view = RegionController.CreateView(region) as Map.RegionView;
                }

                view.UpdateView(region);
            }

            m_Model = element;
        }
    }
}
