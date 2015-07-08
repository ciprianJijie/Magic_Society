using UnityEngine;
using System;

namespace MS.Core
{
	public class InputManager : Singleton<InputManager>
	{
        protected static InputSystem     m_input;

        void Start()
        {
            #if UNITY_STANDALONE || UNITY_EDITOR
            m_input = new DesktopInput();
            #elif UNITY_IOS || UNITY_ANDROID
            m_input = new TouchInput();
            #else
            MS.Debug.Core.LogError("No input system implemented for this platform");
            #endif
        }

        public static Vector3 CursorPosition
        {
            get
            {
                return m_input.CursorPosition;
            }
        }

        public static bool GetButton(string name)
        {
            return m_input.GetButton(name);
        }
	}
}

