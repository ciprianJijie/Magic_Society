using UnityEngine;

namespace MS
{
    public static class Events
    {
        // Default Actions
        public static void DefaultAction() {}
        public static void DefaultAction(string text) {}
        public static void DefaultAction(int x, int y) {}
        public static void DefaultAction(Model.Player player) {}
        public static void DefaultAction(Model.City city) {}
        public static void DefaultAction(Model.Kingdom.Building building) {}
        public static void DefaultAction(Model.Kingdom.BuildingQueueItem item) {}
        public static void DefaultAction(int value) {}
        public static void DefaultAction(Model.Personality personality) {}
        public static void DefaultAction(Model.Phase phase) {}
        public static void DefaultAction(Model.Turn turn) {}
		public static void DefaultAction(Model.World.Region region) {}

        // Events Types
        public delegate void Event();
        public delegate void StringEvent(string text);
        public delegate void GridPositionEvent(int x, int y);
        public delegate void PlayerEvent(Model.Player player);
        public delegate void CityEvent(Model.City city);
        public delegate void BuildingEvent(Model.Kingdom.Building building);
        public delegate void BuildingQueueItemEvent(Model.Kingdom.BuildingQueueItem item);
        public delegate void ValueEvent(int value);
        public delegate void PersonalityEvent(Model.Personality personality);
        public delegate void PhaseEvent(Model.Phase phase);
        public delegate void TurnEvent(Model.Turn turn);
		public delegate void RegionEvent(Model.World.Region region);
    }
}