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
        public MS.Events.GridPositionEvent          OnTileHover         =   MS.Events.DefaultAction;
        public MS.Events.Event                      OnTileHoverEnds     =   MS.Events.DefaultAction;
        public MS.Events.CityEvent                  OnCitySelected      =   MS.Events.DefaultAction;
        public MS.Events.Event                      OnCityDeselected    =   MS.Events.DefaultAction;
        public MS.Events.Event                      OnPersonalitiesMenu =   MS.Events.DefaultAction;

        private readonly static float               m_TimeToShowTileInformation = 1.25f;
        private float                               m_TimeHoveringTile;
        private bool                                m_TileHoverEventSent = false;
        private Vector2                             m_LastHoveredTile;
        private bool                                m_CitySelected;

        protected void LateUpdate()
        {
            if (EventSystem.IsPointerOverGameObject() == false)
            {
                if (m_LastHoveredTile == MouseToGrid.LastGridPosition)
                {
                    m_TimeHoveringTile += Time.deltaTime;

                    if (m_TimeHoveringTile >= m_TimeToShowTileInformation && m_TileHoverEventSent == false)
                    {
                        m_TileHoverEventSent = true;
                        OnTileHover((int)m_LastHoveredTile.x, (int)m_LastHoveredTile.y);
                    }
                }
                else
                {
                    m_TimeHoveringTile = 0f;
                    m_TileHoverEventSent = false;
                    OnTileHoverEnds();

                }
                m_LastHoveredTile = MouseToGrid.LastGridPosition;

                if (InputManager.GetButton("Select"))
                {
                    Model.City          city;
                    Vector2             axialPosition;

                    axialPosition = MouseToGrid.LastGridPosition;
                    city = Model.Game.Instance.World.GetRegion(axialPosition).CapitalArea.Element as Model.City;
                    

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
    }
}