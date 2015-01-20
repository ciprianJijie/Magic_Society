using System;
using UnityEngine;
using SimpleJSON;

namespace MS.Model
{
    public class City : MapElement
    {
        public City(JSONNode json)
            : base(json["location"]["x"].AsInt, json["location"]["y"].AsInt)
        {

        }

        public override void FromJSON(SimpleJSON.JSONNode node)
        {
            throw new NotImplementedException();
        }

        public override SimpleJSON.JSONNode ToJSON()
        {
            throw new NotImplementedException();
        }
    }
}

