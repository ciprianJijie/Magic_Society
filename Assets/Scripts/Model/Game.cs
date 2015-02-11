using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MS.Model
{
	public class Game : ModelElement
	{
		protected Scenario m_currentScenario;

		public Scenario Scenario
		{
			get
			{
				return m_currentScenario;
			}

			set
			{
				m_currentScenario = value;
			}
		}

        public override void FromJSON(SimpleJSON.JSONNode node)
        {
            m_currentScenario = new Scenario();

			m_currentScenario.FromJSON(node);
        }

        public override SimpleJSON.JSONNode ToJSON()
        {
            return m_currentScenario.ToJSON();
        }
	}
}
