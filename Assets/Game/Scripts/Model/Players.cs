using UnityEngine;
using SimpleJSON;
using System.Collections.Generic;
using System.Collections;

namespace MS.Model
{
    public class Players : ModelElement, IEnumerable<Player>
	{
		protected List<Player> m_Players;
        protected NeutralPlayer m_NeutralPlayer;

        public NeutralPlayer NeutralPlayer
        {
            get { return m_NeutralPlayer; }
        }

        public Players()
        {
            m_Players = new List<Player>();
            m_NeutralPlayer = new NeutralPlayer();
            m_Players.Add(m_NeutralPlayer);
        }

        public Player Find(string name)
        {
            foreach (Player player in m_Players)
            {
                if (player.Name == name)
                {
                    return player;
                }
            }
            return null;
        }

        public void AddPlayer(Player player)
        {
            m_Players.Add(player);
        }

        public bool RemovePlayer(string name)
        {
            Player player;

            player = Find(name);

            if (player != null)
            {
                m_Players.Remove(player);
                return true;
            }

            return false;
        }

        public override void FromJSON(JSONNode json)
        {
            Player player;

            foreach (JSONNode node in json.AsArray)
            {
                player = Player.Create(node["type"]);

                player.FromJSON(node);
                m_Players.Add(player);
            }
        }

        public override JSONNode ToJSON()
        {
            JSONArray array = new JSONArray();

            foreach (Player player in m_Players)
            {
                if (player is NeutralPlayer == false)
                {
                    array.Add(player.ToJSON());
                }                
            }

            return array;
        }

        // IEnumerable

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_Players.GetEnumerator();
        }

        IEnumerator<Player> IEnumerable<Player>.GetEnumerator()
        {
            return m_Players.GetEnumerator();
        }
	}
}