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
            throw new System.NotImplementedException();
        }

        public override SimpleJSON.JSONNode ToJSON()
        {
            throw new System.NotImplementedException();
        }
	}
}