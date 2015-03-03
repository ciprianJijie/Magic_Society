using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

namespace MS.Model
{
    public abstract class Player : ModelElement
    {
        #region Constructors, Destructor, ...

        public Player(string name)
        {
            m_name = name;
            m_resources = CreateResourceStorage();
        }

        public Player(JSONNode json)
        {
            m_resources = CreateResourceStorage();
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

        public List<ResourceStorage> CreateResourceStorage()
        {
            List<ResourceStorage> storage;

            storage = new List<ResourceStorage>();

            foreach (GameResource resource in Manager.GameManager.Game.Scenario.Map.Resources)
            {
                storage.Add(new ResourceStorage(resource, 0));
            }

            return storage;
        }

        #endregion

        #region Public methods

        public void AddResource(string name, int amount)
        {
            ResourceStorage resource;

            resource = m_resources.Find(i => i.Resource.Name == name);

            if (resource != null)
            {
                resource.Add(amount);
            }
            else
            {
                throw new MS.Exceptions.ResourceNotFound(name);
            }

        }

        public override string ToString()
        {
            string resourcesStr = "";

            foreach (ResourceStorage resource in m_resources)
            {
                resourcesStr += resource.Resource.Name + ":" + resource.Amount + " ";
            }

            return string.Format("[Player: Name={0}, Resources={1}]", Name, resourcesStr);
        }
        #endregion

        #region Private methods


        #endregion

        #region Properties

        public string Name { get { return m_name; } set { m_name = value; } }

        #endregion

        #region Attributes

        protected   string                  m_name  =   "No name";
        protected   List<ResourceStorage>   m_resources;

        #endregion
    }
}