using UnityEngine;
using SimpleJSON;
using System;

namespace MS.Model
{
    public class MapElement : ModelElement
    {
        // Attributes
        public int X;
        public int Y;
        // ---

        public MapElement()
        {
            // Defaul values
            Name = "No name";
            X = 0;
            Y = 0;
        }

        public MapElement(int x, int y, string name)
        {
            Name = name;
            X = x;
            Y = y;
        }

        public MapElement(MapElement other)
        {
            Name = other.Name;
            X = other.X;
            Y = other.Y;
        }

        public MapElement(int x, int y, MapElement other)
        {
            Name = other.Name;
            X = x;
            Y = y;
        }

        public override void FromJSON(JSONNode json)
        {
            base.FromJSON(json);
            X = json["x"].AsInt;
            Y = json["y"].AsInt;

        }

        public override JSONNode ToJSON()
        {
            JSONNode root;

            root = base.ToJSON();

            root.Add("x", new JSONData(X));
            root.Add("y", new JSONData(Y));

            return root;
        }

        // Factory Methods

        public static MapElement Create(int x, int y, string name)
        {
            MapElement element;

            if (name == "MAP_ELEMENT_FOREST")
            {
                element = new Forest();
            }
            else if (name == "Stone Deposits")
            {
                element = new StoneDeposits();
            }
            else if (name == "Gold Deposits")
            {
                element = new GoldDeposits();
            }
            else if (name == "City")
            {
                element = new City();
            }
            else
            {
                return null;
            }

            element.X = x;
            element.Y = y;

            return element;
        }

        // ---
    }
}
