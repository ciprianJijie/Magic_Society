using UnityEngine;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System;

namespace MS.Model
{
    public class Player : ModelElement, IResourceWarehouse
    {
        public int Gold;
        public int Research;

        protected ResourceAdvancedAmount m_GoldCollected;
        protected ResourceAdvancedAmount m_ResearchCollected;

        public int GoldCollected
        {
            get
            {
                return m_GoldCollected.GetTotalAmount();
            }
        }

        public int ResearchCollected
        {
            get
            {
                return m_ResearchCollected.GetTotalAmount();
            }
        }

        public Player()
        {
            Name                =   "Unnamed";
            m_GoldCollected     =   new ResourceAdvancedAmount();
            m_ResearchCollected =   new ResourceAdvancedAmount();
        }

        public Player(string name)
        {
            Name                =   name;
            m_GoldCollected     =   new ResourceAdvancedAmount();
            m_ResearchCollected =   new ResourceAdvancedAmount();
        }

        public virtual void Play<T>(T phase) where T: Phase
        {
            phase.Finish();
        }

        public void Store(ResourceAmount amount)
        {
            if (amount.Resource is Gold)
            {
                m_GoldCollected.AddAmount(amount);
                Gold += amount.Amount;
            }
            else if (amount.Resource is Research)
            {
                m_ResearchCollected.AddAmount(amount);
                Research += amount.Amount;
            }
        }

        public void Store(ResourceAdvancedAmount amount)
        {
            foreach (ResourceAmount singleAmount in amount)
            {
                Store(singleAmount);
            }
        }

        public void ClearCollectedCache()
        {
            m_GoldCollected.Clear();
            m_ResearchCollected.Clear();
        }

        public override void FromJSON(JSONNode json)
        {
            base.FromJSON(json);

            Gold        =   json["gold"].AsInt;
            Research    =   json["research"].AsInt;
        }

        public override JSONNode ToJSON()
        {
            JSONNode root = base.ToJSON();

            root.Add("name", Name);
            root.Add("gold", new JSONData(Gold));
            root.Add("research", new JSONData(Research));

            return root;
        }

        public static Player Create(string type)
        {
            Player player;

            switch (type)
            {
                case "AI":
                    player = new AIPlayer();
                    break;
                default:
                    player = new HumanPlayer();
                    break;
            }

            return player;
        }
    }
}
