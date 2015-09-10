using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

namespace MS.Model.Kingdom
{
    public class BuildingQueue : ModelElement, IEnumerable<BuildingQueueItem>
    {
        public static readonly float PRODUCTION_TO_GOLD_RATIO = 2.0f;

        public City City;

        protected List<BuildingQueueItem> m_Queue;

        public Events.BuildingEvent OnBuildingCompleted = Events.DefaultAction;

        public BuildingQueue()
        {

        }

        public BuildingQueue(City city)
        {
            City = city;
        }

        public void Enqueue(Building building)
        {
            BuildingQueueItem item;

            item = new BuildingQueueItem(0, building);

            item.OnFinished += OnItemCompleted;

            m_Queue.Add(item);
        }

        public void Remove(int index)
        {
            m_Queue[index].OnFinished -= OnItemCompleted;

            m_Queue.RemoveAt(index);
        }

        public void OnItemCompleted(BuildingQueueItem item)
        {
            int goldGenerated;

            m_Queue.Remove(item);
            goldGenerated = Mathf.RoundToInt(item.RemainingProduction * PRODUCTION_TO_GOLD_RATIO);

            City.Owner.Gold += goldGenerated;

            OnBuildingCompleted(item.Building);
        }

        public override void FromJSON(JSONNode json)
        {
            JSONArray queue;

            queue = json.AsArray;

            foreach (JSONNode node in queue)
            {
                BuildingQueueItem item;

                item = new BuildingQueueItem();
                item.FromJSON(node);
                m_Queue.Add(item);
            }
        }

        public override JSONNode ToJSON()
        {
            JSONArray array;

            array = new JSONArray();

            foreach (BuildingQueueItem item in m_Queue)
            {
                array.Add(item.ToJSON());
            }

            return array;
        }

        public IEnumerator<BuildingQueueItem> GetEnumerator()
        {
            return m_Queue.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_Queue.GetEnumerator();
        }
    }
}
