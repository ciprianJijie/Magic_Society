using System;
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
            string type;

            type = node["type"].Value;

            if (type == "City")
            {
                return new City(node);
            }
            else if (type == "Pickable Resource")
            {
                return new PickableResource(node);
            }
            else if (type == "Resource Producer")
            {
                return new ResourceProducer(node);
            }

            throw new Exceptions.FactoryMethodWrongType(node["type"].Value);
        }

        public Vector2 Location;
    }
}
