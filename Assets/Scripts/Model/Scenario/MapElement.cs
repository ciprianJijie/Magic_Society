﻿using System;
using UnityEngine;
using SimpleJSON;

namespace MS.Model
{
    public abstract class MapElement : ModelElement
    {
        public MapElement(int x, int y)
        {
            Location = new Vector2(x, y);
        }

        public static MapElement Create(JSONNode node)
        {
            if (node["type"].Value == "City")
            {
                return new City(node);
            }

            throw new Exceptions.FactoryMethodWrongType(node["type"].Value);
        }

        public Vector2 Location;
    }
}

