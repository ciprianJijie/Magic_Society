using UnityEngine;
using UnityEngine.UI;

namespace MS
{
    /// <summary>
    /// Translates the input received by the InputManager into game actions.
    /// </summary>
    public class GameInputManager : MonoBehaviour
    {
        public Core.InputManager                    InputManager;
        public UnityEngine.EventSystems.EventSystem EventSystem;
        public MouseToGrid                          MouseToGrid;

        // Events
        public Events.GridPositionEvent          	OnTileHover         =   Events.DefaultAction;
        public Events.Event                      	OnTileHoverEnds     =   Events.DefaultAction;
		public Events.RegionEvent 					OnRegionHover 		= 	Events.DefaultAction;
		public Events.RegionEvent					OnRegionHoverEnds 	= 	Events.DefaultAction;
        public Events.CityEvent                  	OnCitySelected      =   Events.DefaultAction;
        public Events.Event                      	OnCityDeselected    =   Events.DefaultAction;
        public Events.Event                      	OnPersonalitiesMenu =   Events.DefaultAction;

        private bool                                m_CitySelected;
		private bool 								m_RegionHovered;
		private Model.World.Region					m_LastHoveredRegion;
		private readonly float 						m_TimeToHover = 1.0f;
		private float 								m_TimeHovered;

        protected void LateUpdate()
        {
            if (EventSystem.IsPointerOverGameObject() == false)
            {
				// Region Hovering
				Model.World.Region  region;
				Vector2             axialPosition;
				Vector3 			cubePosition;

				axialPosition   =   MouseToGrid.LastGridPosition;
				cubePosition 	= 	Hexagon.AxialToCube(axialPosition);
				region          =   Model.Game.Instance.World.GetRegion(cubePosition);

				CheckRegionHover(region);

                if (InputManager.GetButton("Select"))
                {
                    Model.City city;
                    
                    city = region.CapitalArea.Element as Model.City;

                    if (city != null)
                    {
                        if (city.Owner == Model.Game.Instance.Turns.CurrentTurn.Player)
                        {
                            Managers.GameManager.Instance.SelectedCity = city;
                            OnCitySelected(city);
                            m_CitySelected = true;
                        }
                    }
                    else if (m_CitySelected)
                    {
                        Managers.GameManager.Instance.SelectedCity = null;
                        OnCityDeselected();
                        m_CitySelected = false;
                    }
                }

                if (InputManager.GetButton("Personalities Menu"))
                {
                    OnPersonalitiesMenu();
                }
            }
        }

		protected void CheckRegionHover(Model.World.Region region)
		{
			if (region != null)
			{
				if ((region == m_LastHoveredRegion && m_RegionHovered == false) || m_LastHoveredRegion == null)
				{
					m_TimeHovered 			+= 	Time.deltaTime;

					if (m_TimeHovered >= m_TimeToHover)
					{
						m_RegionHovered = true;
						OnRegionHover(region);
					}
				}
				else if (m_RegionHovered == true && region != m_LastHoveredRegion)
				{
					m_RegionHovered = false;
					m_TimeHovered = 0f;
					OnRegionHoverEnds(m_LastHoveredRegion);
				}
			}
			m_LastHoveredRegion = region;
		}
    }
}
