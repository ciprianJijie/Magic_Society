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
        }
    }
}