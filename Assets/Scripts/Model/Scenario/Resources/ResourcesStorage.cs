using UnityEngine;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MS.Manager;

namespace MS.Model
{
    ///<summary>Represents any storage of a certain resource, from the players resources to map deposits.</summary>
    public class ResourceStorage : ModelElement
    {
        public ResourceStorage(GameResource resource)
        {
            Resource = resource;
            m_amount = 0;
        }

        public ResourceStorage(GameResource resource, int amount)
        {
            Resource = resource;
            m_amount = amount;
        }

        public ResourceStorage(JSONNode json)
        {
            FromJSON(json);
        }

        public override void FromJSON(JSONNode json)
        {
            Resource = GameManager.Game.Scenario.Map.Resources.Where(i => i.Name == json["resource"].Value) as GameResource;
            m_amount = json["amount"].AsInt;
        }

        public override JSONNode ToJSON()
        {
            JSONNode json = new JSONNode();

            json["resource"]    =   Resource.Name;
            json["amount"]      =   m_amount.ToString();

            return json;
        }


        public GameResource Resource;
        private int         m_amount;
    }
}
