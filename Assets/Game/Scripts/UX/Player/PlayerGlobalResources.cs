using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MS.UI
{
    public class PlayerGlobalResources : Singleton<PlayerGlobalResources>
    {
        public Text GoldLabel;
        public Text ResearchLabel;

        protected Model.Player m_Player;

        public void UpdateValues(int gold, int goldProduction, int research, int researchProduction)
        {
            GoldLabel.text = FormatResource(gold, goldProduction);
            ResearchLabel.text = FormatResource(research, researchProduction);
        }

        public void UpdateGold(int gold, int modifier)
        {
            GoldLabel.text = FormatResource(gold, modifier);
        }

        public void UpdateResearch(int research, int modifier)
        {
            ResearchLabel.text = FormatResource(research, modifier);
        }

        protected string FormatResource(int value, int modifier)
        {
            return string.Format("{0} ({1})", value, modifier);
        }
    }
}
