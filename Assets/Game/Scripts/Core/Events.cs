using UnityEngine;

namespace MS
{
    public static class Events
    {
        // Default Actions
        public static void DefaultAction() {}
        public static void DefaultAction(string text) {}
        public static void DefaultAction(int x, int y) {}

        // Events Types
        public delegate void Event();
        public delegate void StringEvent(string text);
        public delegate void GridPositionEvent(int x, int y);
    }
}