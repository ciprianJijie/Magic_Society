using System;
using UnityEngine;

namespace MS.Model
{
    public abstract class MapElement : ModelElement
    {
        public MapElement(int x, int y)
        {
            Position = new Vector2(x, y);
        }

        public Vector2 Position;
    }
}

