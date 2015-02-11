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
            MS.Debug.Core.Log("Trying to load resource " + json["resource"]);
            Resource = GameManager.Game.Scenario.Map.GetResource(json["resource"].Value);
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
