using UnityEngine;
using SimpleJSON;
using System.Collections.Generic;

namespace MS
{
	public class Players : ModelElement
	{
		protected List<Player> m_Players;

        public Players()
        {
            m_Players = new List<Player>();
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
                player = new Player();

                player.FromJSON(node);
                m_Players.Add(player);
            }
        }

        public override JSONNode ToJSON()
        {
            JSONArray array = new JSONArray();

            foreach (Player player in m_Players)
            {
                array.Add(player.ToJSON());
            }

            return array;
        }
	}
}