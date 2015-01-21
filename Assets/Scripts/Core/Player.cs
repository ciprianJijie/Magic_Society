using UnityEngine;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

namespace MS.Model
{
    public abstract class Player : ModelElement
    {
        #region Constructors, Destructor, ...

        public Player(JSONNode json)
        {
            FromJSON(json);
        }

        #endregion

        #region Static methods
        /// <summary>
        /// Factory method to create different types of players.
        /// </summary>
        /// <param name="json">Json.</param>
        public static Player Create(JSONNode json)
        {
            if (json["type"].Value == "Human")
            {
                return new HumanPlayer(json);
            }
            else if (json["type"].Value == "AI")
            {
                return new AIPlayer(json);
            }
            throw new Exceptions.FactoryMethodWrongType(json["type"]);
        }

        #endregion

        #region Public methods
        public override string ToString()
        {
            return string.Format("[Player: Name={0}]", Name);
        }
        #endregion

        #region Private methods


        #endregion

        #region Properties

        [XmlAttribute("name")]
        public string Name { get { return m_name; } set { m_name = value; } }

        #endregion

        #region Attributes

        protected   string              m_name  =   "No name";

        #endregion
    }
}