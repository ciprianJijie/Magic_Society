using System.Collections.Generic;
using MS.Model.World;
using UnityEngine;

namespace MS.Views.World.Map
{
    public class RegionView : View<Model.World.Region>
    {
        public Controllers.World.Map.AreaController         AreaController;
        public Controllers.World.Map.CentralAreaController  CentralAreaController;
        public float Size;

        protected List<Views.World.Map.AreaView> m_AreaViews;

        public override void UpdateView(Region element)
        {
            CentralAreaView     centralView;
            AreaView            areaView;
            Vector2             worldPosition;
            Vector3             finalPosition;

            AreaController.Holder           =   this.transform;
            CentralAreaController.Holder    =   this.transform;
            centralView                     =   CentralAreaController.FindView(element.CapitalArea);

            if (centralView == null)
            {
                centralView = CentralAreaController.CreateView(element.CapitalArea) as CentralAreaView;
            }

            centralView.UpdateView(element.CapitalArea);

            foreach (Area area in element)
            {
                areaView = AreaController.FindView(area);

                if (areaView == null)
                {
                    areaView = AreaController.CreateView(area) as AreaView;
                }

                areaView.UpdateView(area);
            }

            worldPosition = Hexagon.CubeToWorld(element.CubePosition, Size);

            finalPosition = new Vector3(worldPosition.x, 0.0f, worldPosition.y);

            this.transform.position = finalPosition;

            this.gameObject.name = "Region (" + element.CubePosition + ")";
        }
    }
}
