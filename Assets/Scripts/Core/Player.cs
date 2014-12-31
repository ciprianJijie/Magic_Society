using UnityEngine;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace MS.Model
{
    [Serializable, XmlRoot("Player")]
    public class Player
    {
        #region Constructors, Destructor, ...
        public Player()
        {
        }

        public Player(string name)
        {
            m_name = name;
        }
        #endregion

        #region Static methods
        public static void ToXML(Player player, string filePath)
        {
            XmlSerializer serializer;
            string path;
            FileStream stream;

            serializer  =   new XmlSerializer(typeof(Player));
            path        =   Application.dataPath + filePath;
            stream      =   new FileStream(path, FileMode.Create);

            MS.Debug.Core.Log("Writting file " + path);

            serializer.Serialize(stream, player);
            stream.Close();
        }

        public static Player FromXML(string filePath)
        {
            XmlSerializer serializer;
            FileStream stream;
            string path;
            Player dataRead;

            serializer  =   new XmlSerializer(typeof(Player));
            path        =   Application.dataPath + filePath;
            stream      =   new FileStream(path, FileMode.Open);

            MS.Debug.Core.Log("Reading file " + path);

            dataRead    =   serializer.Deserialize(stream) as Player;

            stream.Close();

            return dataRead;
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