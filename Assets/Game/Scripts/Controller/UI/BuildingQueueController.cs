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
                var view = SingleItemController.CreateView(item) as Views.UI.BuildingQueueItemView;
                view.UpdateView(item);
                view.OnCancel += OnCancelBuilding;
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

            var view = SingleItemController.CreateView(item) as Views.UI.BuildingQueueItemView;
            view.UpdateView(item);
            view.OnCancel += OnCancelBuilding;

            CityController.UpdateBuildingSchemesArea(GameController.Instance.SelectedCity);
            CityController.BuildingPanelManager.Hide();
        }

        protected void OnCancelBuilding(Model.Kingdom.BuildingQueueItem queueItem)
        {
            var view = SingleItemController.FindView(queueItem) as Views.UI.BuildingQueueItemView;

            view.OnCancel -= OnCancelBuilding;

            SingleItemController.DestroyView(queueItem);
            GameController.Instance.SelectedCity.BuildingQueue.Remove(queueItem.Building.Name);
            CityController.UpdateBuildingSchemesArea(GameController.Instance.SelectedCity);           
        }

        protected void Start()
        {
            BuildingSchemeController.OnBuild += Add;
        }

        protected void OnDestroy()
        {
            BuildingSchemeController.OnBuild -= Add;
        }
    }
}

