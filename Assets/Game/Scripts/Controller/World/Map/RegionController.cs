
using MS.Model.World;

namespace MS.Controllers.World.Map
{
    public class RegionController : Controller<Views.World.Map.RegionView, Model.World.Region>
    {
        public AreaController           AreaController;
        public CentralAreaController    CentralAreaController;

        public override IUpdatableView<Region> CreateView(Region modelElement)
        {
            var view = base.CreateView(modelElement) as Views.World.Map.RegionView;

            view.AreaController = AreaController;
            view.CentralAreaController = CentralAreaController;

            return view;
        }
    }
}
