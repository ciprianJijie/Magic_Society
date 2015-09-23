using UnityEngine;
using System;

namespace MS.Controllers.UI
{
    public class BuildingQueueController : MonoBehaviour
    {
        public BuildingQueueItemController  SingleItemController;
        public BuildingSchemeController     BuildingSchemeController;
        public CityController               CityController;

        protected Model.Kingdom.BuildingQueue m_Queue;

        public void Show(Model.Kingdom.BuildingQueue queue)
        {
            foreach (var item in queue)
            {
                var view = SingleItemController.CreateView(item);
                view.UpdateView(item);
            }

            m_Queue = queue;
        }

        public void Hide()
        {
            SingleItemController.ClearViews();
            m_Queue = null;
        }

        public void Add(Model.Kingdom.Building scheme)
        {
            Model.Kingdom.BuildingQueueItem item;

            m_Queue.Enqueue(scheme);

            item = new MS.Model.Kingdom.BuildingQueueItem(0, scheme);

            var view = SingleItemController.CreateView(item);
            view.UpdateView(item);

            CityController.UpdateBuildingSchemesArea(GameController.Instance.SelectedCity);
        }

        protected void Start()
        {
            BuildingSchemeController.OnBuild += Add;
        }
    }
}

