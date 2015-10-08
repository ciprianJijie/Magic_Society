using MS.Model.World;
using UnityEngine;

namespace MS.Controllers.World
{
    public class WorldController : Controller<Views.World.WorldView, Model.World.World>
    {
        public Controllers.World.Map.RegionController RegionController;

        public override IUpdatableView<Model.World.World> CreateView(Model.World.World modelElement)
        {
            Views.World.WorldView view = base.CreateView(modelElement) as Views.World.WorldView;

            view.RegionController = RegionController;

            return view;
        }
    }
}
