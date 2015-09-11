using UnityEngine;

namespace MS
{
    /// <summary>
    /// Translates the input received by the InputManager into game actions.
    /// </summary>
    public class GameInputManager : MonoBehaviour
    {
        public Core.InputManager    InputManager;
        public MouseToGrid          MouseToGrid;
        public float                CameraSpeed;
        public float                FlyOverDuration;
        public AnimationCurve       FlyOverCurve;

        // Objects
        public Camera               MainCamera;

        // Events
        public MS.Events.GridPositionEvent  OnTileHover         =   MS.Events.DefaultAction;
        public MS.Events.Event              OnTileHoverEnds     =   MS.Events.DefaultAction;
        public MS.Events.CityEvent          OnCitySelected      =   MS.Events.DefaultAction;
        public MS.Events.Event              OnCityDeselected    =   MS.Events.DefaultAction;

        private readonly static float   m_TimeToShowTileInformation = 1.25f;
        private float                   m_TimeHoveringTile;
        private Vector2                 m_LastHoveredTile;
        private bool                    m_CitySelected;

        protected void LateUpdate()
        {
            float horizontal;
            float vertical;
            float zoom;
            float rotation;
            Vector3 forward;
            Vector3 right;
            
            horizontal  =   InputManager.GetAxis("Horizontal");
            vertical    =   InputManager.GetAxis("Vertical");
            zoom        =   InputManager.GetAxis("Mouse ScrollWheel");
            rotation    =   InputManager.GetAxis("Horizontal Rotation");
            forward     =   MainCamera.transform.forward;
            forward.y   =   0f;
            right       =   MainCamera.transform.right;
            right.y     =   0f;
            
            MainCamera.transform.position += forward * vertical * CameraSpeed * Time.deltaTime;
            MainCamera.transform.position += right * horizontal * CameraSpeed * Time.deltaTime;
            MainCamera.transform.position += MainCamera.transform.forward * zoom * 2.0f * CameraSpeed * Time.deltaTime;
            MainCamera.transform.RotateAround(MainCamera.transform.position, Vector3.up, rotation * 10.0f * CameraSpeed * Time.deltaTime);

            if (InputManager.GetButton("FlyOver"))
            {
                Vector3 worldPosition;
                MS.Core.Actions.Movement movement;

                worldPosition = MouseToGrid.GridController.LocalToWorld(MouseToGrid.LastGridPosition);
                worldPosition.y = MainCamera.transform.position.y;

                movement = MS.Core.Actions.ActionsManager.Instance.Create<MS.Core.Actions.Movement>();

                movement.Perform(MainCamera.transform, MainCamera.transform.position, worldPosition, FlyOverDuration, FlyOverCurve);
            }

            if (MouseToGrid.IsValidPosition(MouseToGrid.LastGridPosition))
            {
                if (m_LastHoveredTile == MouseToGrid.LastGridPosition)
                {
                    m_TimeHoveringTile += Time.deltaTime;

                    if (m_TimeHoveringTile >= m_TimeToShowTileInformation)
                    {
                        OnTileHover((int)m_LastHoveredTile.x, (int)m_LastHoveredTile.y);
                    }
                }
                else
                {
                    m_TimeHoveringTile = 0f;
                    OnTileHoverEnds();
                }
                m_LastHoveredTile = MouseToGrid.LastGridPosition;
            }

            if (InputManager.GetButton("Select"))
            {
                Model.City city;

                city = GameController.Instance.Game.Map.Grid.GetElement(MouseToGrid.LastGridPosition) as Model.City;

                if (city != null)
                {
                    if (city.Owner == GameController.Instance.Game.Turns.CurrentTurn.Player)
                    {
                        OnCitySelected(city);
                        m_CitySelected = true;
                    }                    
                }
                else if (m_CitySelected)
                {
                    OnCityDeselected();
                    m_CitySelected = false;
                }
            }
        }
    }
}