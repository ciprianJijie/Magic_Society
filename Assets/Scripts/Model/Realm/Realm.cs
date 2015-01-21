using UnityEngine;
using System.Collections;
using SimpleJSON;

namespace MS.Model
{
    public class Realm : ModelElement
    {
        public Realm()
        {

        }

        public Realm(JSONNode json)
        {
            string playerName;

            playerName = json["owner"].Value;

            try
            {
                m_owner = MS.Manager.GameManager.GetPlayer(playerName);
            }
            catch(Exceptions.PlayerNotFound excp)
            {
                MS.Debug.Core.LogError(excp.Message);
                MS.Debug.Core.LogError("Realm will have no owner.");
            }
        }

        public override void FromJSON(SimpleJSON.JSONNode node)
        {
            throw new System.NotImplementedException();
        }

        public override SimpleJSON.JSONNode ToJSON()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Player that owns this realm.
        /// </summary>
        /// <value>The owner.</value>
        public Player Owner
        {
            get
            {
                return m_owner;
            }
        }

        private Player m_owner;
    }
}
