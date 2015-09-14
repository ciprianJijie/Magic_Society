using UnityEngine;

namespace MS.Controllers.World
{
    public class MapElementController : MonoBehaviour
    {
        public GenericElementController ForestControllerPrefab;
        public GenericElementController GoldDepositsControllerPrefab;
        public GenericElementController StoneDepositsControllerPrefab;
        public Kingdom.CityController   CityControllerPrefab;

        private GameObject m_InstantiatedElement;

        public void UpdateController(Model.MapElement element)
        {
            if (m_InstantiatedElement != null)
            {
                Destroy(m_InstantiatedElement.gameObject);
            }

            if (element is Model.City)
            {
                Kingdom.CityController controller;

                controller = Utils.Instantiate<Kingdom.CityController>(CityControllerPrefab, this.transform, this.transform.position, this.transform.rotation);

                controller.CreateView(element as Model.City);
                controller.UpdateAllViews();

                m_InstantiatedElement = controller.gameObject;
            }
            else
            {
                GenericElementController prefab;

                if (element is Model.Forest)
                {
                    prefab = ForestControllerPrefab;
                }
                else if (element is Model.StoneDeposits)
                {
                    prefab = StoneDepositsControllerPrefab;
                }
                else if (element is Model.GoldDeposits)
                {
                    prefab = GoldDepositsControllerPrefab;
                }
                else
                {
                    prefab = null;
                }

                GenericElementController controller;

                controller = Utils.Instantiate<GenericElementController>(prefab, this.transform, this.transform.position, this.transform.rotation);

                controller.CreateView(element);
                controller.UpdateAllViews();

                m_InstantiatedElement = controller.gameObject;
            }
        }
    }
}
