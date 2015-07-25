using UnityEngine;
using System.Collections;

namespace MS
{
	public class LevelEditorInputManager : Singleton<LevelEditorInputManager>
	{
		/// <summary>Component to translate between mouse position and grid position.</summary>
        public MouseToGrid MouseToGrid;

		/// <summary>Camera to move using the input.</summary>
        public Camera MainCamera;

		/// <summary>Special tile used to identify what tile the cursor is hovering</summary>
        public Transform TileSelector;

		[RangeAttribute(0.1f, 8.0f)]
        public float MovementSpeed;

		// Events

		protected void OnMouseMove(int x, int y)
		{
            // Move the selector
            TileSelector.position = MouseToGrid.GridController.GetSelectorPosition(x, y);
        }

		protected void OnMouseLeftClick(int x, int y)
		{

		}

		protected void OnMouseRightClick(int x, int y)
		{

		}

		// Unity Methods

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

		protected void Start()
		{
			MouseToGrid.OnMouseOver += OnMouseMove;
			MouseToGrid.OnMouseLeftClick += OnMouseLeftClick;
			MouseToGrid.OnMouseRightClick += OnMouseRightClick;
		}

    }
}
