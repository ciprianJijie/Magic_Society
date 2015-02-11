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
            m_population = json["population"].AsInt;
            MS.Debug.Core.Log("Loaded city at " + Location + "(" + m_population + ")");
        }

        public override void FromJSON(SimpleJSON.JSONNode json)
        {
            this.Location       =   new Vector2(json["location"]["x"].AsInt, json["location"]["y"].AsInt);
            this.m_population   =   json["population"].AsInt;
        }

        public override SimpleJSON.JSONNode ToJSON()
        {
            JSONNode json = new JSONNode();

            json["location"]["x"]   =   this.Location.x.ToString();
            json["location"]["y"]   =   this.Location.y.ToString();
            json["population"]      =   this.m_population.ToString();

            return json;
        }

        #region Properties

        /// <summary>
        /// How many units of population this city has
        /// </summary>
        /// <value>The population.</value>
        public int Population
        {
            get
            {
                return m_population;
            }
        }

        #endregion

        #region Attributes

        private int m_population;

        #endregion
    }
}
