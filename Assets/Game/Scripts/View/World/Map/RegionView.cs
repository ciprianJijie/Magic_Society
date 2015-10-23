using System.Collections.Generic;
using MS.Model.World;
using UnityEngine;
using MS.ExtensionMethods;

namespace MS.Views.World.Map
{
    public class RegionView : View<Model.World.Region>
    {
        public Controllers.World.Map.AreaController         AreaController;
        public Controllers.World.Map.CentralAreaController  CentralAreaController;
        public Controllers.Kingdom.CityController           CityController;
        public float Size;

        protected List<Views.World.Map.AreaView> m_AreaViews;
        protected CityView m_CityView;

        public override void UpdateView(Region element)
        {
            CentralAreaView     centralView;
            AreaView            areaView;
            Vector2             worldPosition;
            Vector3             finalPosition;
            int                 areaCount;

            AreaController.Holder           =   this.transform;
            CentralAreaController.Holder    =   this.transform;
            centralView                     =   CentralAreaController.FindView(element.CapitalArea);

            if (centralView == null)
            {
                centralView = CentralAreaController.CreateView(element.CapitalArea) as CentralAreaView;
            }

            centralView.UpdateView(element.CapitalArea);

            if (element.CapitalArea.Element is Model.City)
            {
                // City
                Vector3 position;

                if (m_CityView == null)
                {
                    m_CityView = CityController.CreateView(element.CapitalArea.Element as Model.City) as CityView;
                }

                position = Hexagon.CubeToWorld(element.CubePosition, Size);
                position = position.SwappedYZ();
                m_CityView.transform.position = position;

                m_CityView.UpdateView(element.CapitalArea.Element as Model.City);
            }
            else
            {
                if (m_CityView != null)
                {
                    Destroy(m_CityView.gameObject);
                }
            }

            areaCount = 0;

            foreach (Area area in element)
            {
                areaView = AreaController.FindView(area);

                if (areaView == null)
                {
                    areaView = AreaController.CreateView(area) as AreaView;

                    areaView.transform.Rotate(new Vector3(0.0f, 60.0f * areaCount, 0.0f));
                    areaCount++;
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
