using UnityEngine;
using MS.Model;
using MS.Controllers.World;
using MS.Controllers.Kingdom;

namespace MS
{
    public class MapElementsManager : Singleton<MapElementsManager>
    {
        public GridController   GridController;
        public Transform        ElementsHolder;

        // Controllers for each element type
        public Controllers.World.GenericElementController   ForestControllerPrefab;
        public Controllers.World.GenericElementController   StoneDepositsControllerPrefab;
        public Controllers.World.GenericElementController   GoldDepositsControllerPrefab;
        public Controllers.Kingdom.CityController           CityControllerPrefab;

        protected Grid  m_Grid;
        protected GameObject[,] m_Controllers;

        protected void Start()
        {
                      
        }

        public GameObject CreateController(MapElement element)
        {
            Vector3     position;
            GameObject  prefab;

            position = GridController.LocalToWorld(element.X, element.Y);

            if (element is Forest)
            {
                prefab = ForestControllerPrefab.gameObject;
            }
            else if (element is StoneDeposits)
            {
                prefab = StoneDepositsControllerPrefab.gameObject;
            }
            else if (element is GoldDeposits)
            {
                prefab = GoldDepositsControllerPrefab.gameObject;
            }
            else if (element is City)
            {
                prefab = CityControllerPrefab.gameObject;
            }
            else
            {
                prefab = null;
            }

            GameObject controllerObj = Utils.Instantiate(prefab, ElementsHolder, position, ElementsHolder.rotation);

            // Try not to modify variables outside the method to avoid surprises
            //m_Controllers[element.X, element.Y] = controllerObj;

            return controllerObj;
        }

        public bool DestroyController(MapElement element)
        {
            GameObject controller;

            controller = m_Controllers[element.X, element.Y];

            if (controller != null)
            {
                Destroy(controller.gameObject);
                m_Controllers[element.X, element.Y] = null;
                return true;
            }
            return false;
        }

        public IViewCreator<T> FindController<T>(T element) where T: Model.MapElement
        {
            return m_Controllers[element.X, element.Y] as IViewCreator<T>;
        }
        
        public void UpdateViews(Vector2 position)
        {
            GameObject controller;

            controller = m_Controllers[(int)position.x, (int)position.y];

            if (controller != null)
            {
                controller.GetComponent<IViewUpdater>().UpdateAllViews();
            }
        }

        public GameObject FindController(MapElement element)
        {
            return m_Controllers[element.X, element.Y];
        }

        public void Show(Grid grid)
        {
            // TODO: Remove all previous controllers

            m_Grid = grid;
            m_Controllers = new GameObject[m_Grid.HorizontalSize, m_Grid.VerticalSize];

            for (int x = 0; x < m_Grid.HorizontalSize; ++x)
            {
                for (int y = 0; y < m_Grid.VerticalSize; ++y)
                {
                    MapElement element;

                    element = m_Grid.GetElement(x, y);

                    if (element != null)
                    {
                        GameObject  controllerObj;
                        IController controller;

                        controllerObj           =   CreateController(element);
                        m_Controllers[x, y]     =   controllerObj;
                        controller              =   controllerObj.GetComponent<IController>();

                        controller.CreateView(element);
                        controller.UpdateAllViews();                        
                    }
                }
            }
        }
    }
}
