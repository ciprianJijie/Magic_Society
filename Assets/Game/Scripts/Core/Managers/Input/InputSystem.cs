using UnityEngine;
using System;

namespace MS.Core
{
    public abstract class InputSystem
    {
        public abstract Vector3 CursorPosition
        {
            get;
        }

        public abstract bool GetButton(string name);
    }
}

