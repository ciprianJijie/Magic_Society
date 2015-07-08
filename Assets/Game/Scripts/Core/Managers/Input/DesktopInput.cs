using System;

namespace MS.Core
{
    public class DesktopInput : InputSystem
    {
        public override UnityEngine.Vector3 CursorPosition
        {
            get
            {
                return UnityEngine.Input.mousePosition;
            }
        }

        public override bool GetButton(string name)
        {
            return UnityEngine.Input.GetButton(name);
        }
    }
}

