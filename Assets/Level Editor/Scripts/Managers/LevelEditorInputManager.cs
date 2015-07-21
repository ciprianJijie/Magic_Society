using UnityEngine;
using System.Collections;

namespace MS
{
	public class LevelEditorInputManager : Singleton<LevelEditorInputManager>
	{
        public Camera MainCamera;

		[RangeAttribute(0.1f, 8.0f)]
        public float MovementSpeed;

        protected void LateUpdate()
		{
            float horizontal;
            float vertical;
            float zoom;
            float rotation;
            Vector3 forward;
            Vector3 right;

            horizontal 	= 	Input.GetAxis("Horizontal");
            vertical 	= 	Input.GetAxis("Vertical");
            zoom 		= 	Input.GetAxis("Mouse ScrollWheel");
            rotation 	= 	Input.GetAxis("Horizontal Rotation");
            forward 	= 	MainCamera.transform.forward;
            forward.y 	= 	0f;
            right 		= 	MainCamera.transform.right;
            right.y 	= 	0f;

            MainCamera.transform.position += forward * vertical * MovementSpeed * Time.deltaTime;
            MainCamera.transform.position += right * horizontal * MovementSpeed * Time.deltaTime;
            MainCamera.transform.position += MainCamera.transform.forward * zoom * 2.0f * MovementSpeed * Time.deltaTime;
			MainCamera.transform.RotateAround(MainCamera.transform.position, Vector3.up, rotation * 10.0f * MovementSpeed * Time.deltaTime);
        }
    }
}
