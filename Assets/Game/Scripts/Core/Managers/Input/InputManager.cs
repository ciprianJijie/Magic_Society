using UnityEngine;
using System;

namespace MS.Core
{
    /// <summary>
    /// Abstracts the platform in terms of input. This mediates between the Unity input system and the game input system.
    /// </summary>
	public class InputManager : Singleton<InputManager>
	{
        protected InputSystem    m_input;

        protected void Start()
        {
            #if UNITY_STANDALONE || UNITY_EDITOR
            m_input = new DesktopInput();
            #elif UNITY_IOS || UNITY_ANDROID
            m_input = new TouchInput();
            #else
            MS.Debug.Core.LogError("No input system implemented for this platform");
            #endif
        }

        public Vector3 CursorPosition
        {
            get
            {
                return m_input.CursorPosition;
            }
        }

        public bool GetButton(string name)
        {
            return m_input.GetButton(name);
        }

        public float GetAxis(string name)
        {
            return m_input.GetAxis(name);
        }
	}
}

