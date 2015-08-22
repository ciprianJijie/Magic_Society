using UnityEngine;

namespace MS
{
    public static class Events
    {
        // Default Actions
        public static void DefaultAction() {}
        public static void DefaultAction(string text) {}

        // Events Types
        public delegate void Event();
        public delegate void StringEvent(string text);
    }
}