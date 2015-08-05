using UnityEngine;
using System;

namespace MS.Core
{
    public class TouchInput : InputSystem
    {
        public override UnityEngine.Vector3 CursorPosition
        {
            get
            {
                return Input.GetTouch(0).position;
            }
        }

        public override bool GetButton(string name)
        {
            if (name == "Touch")
            {
                if (Input.touchCount > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public override float GetAxis(string name)
        {
            throw new NotImplementedException();
        }
    }
}

