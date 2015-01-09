using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MS.Model
{
	public class Game
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
	}
}