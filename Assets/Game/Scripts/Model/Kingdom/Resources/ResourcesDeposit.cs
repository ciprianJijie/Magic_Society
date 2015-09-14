using System;
using System.Collections.Generic;

namespace MS.Model
{
    public class ResourcesDeposit : ModelElement
    {
        private Dictionary<Resource, int> m_Resources;

        public ResourcesDeposit()
        {
            m_Resources = new Dictionary<Resource, int>();

            m_Resources.Add(GameController.Instance.Game.Resources.Food, 0);
        }

        public int Add(Resource resource, int amount)
        {
            m_Resources[resource] += amount;

            return m_Resources[resource];
        }

        public override void FromJSON(SimpleJSON.JSONNode json)
        {
            throw new NotImplementedException();
        }

        public override SimpleJSON.JSONNode ToJSON()
        {
            throw new NotImplementedException();
        }
    }
}

