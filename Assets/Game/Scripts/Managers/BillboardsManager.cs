using UnityEngine;

namespace MS.Managers
{
    public class BillboardsManager : Singleton<BillboardsManager>
    {
        public enum Axis { up, down, left, right, forward, back };

        public static Vector3   TargetOrientation;
        public static Vector3   PositionOffset;
        public Axis             PivotAxis;

        protected Camera referenceCamera;

        // return a direction based upon chosen axis
        public Vector3 GetAxis(Axis refAxis)
        {
            switch (refAxis)
            {
                case Axis.down:
                    return Vector3.down;
                case Axis.forward:
                    return Vector3.forward;
                case Axis.back:
                    return Vector3.back;
                case Axis.left:
                    return Vector3.left;
                case Axis.right:
                    return Vector3.right;
            }
            
            return Vector3.up;
        }

        protected override void Awake()
        {
            base.Awake();
            if (!referenceCamera)
                referenceCamera = Camera.main;
        }
        
        void Start()
        {
            PositionOffset      =   referenceCamera.transform.rotation * Vector3.back;
            TargetOrientation   =   referenceCamera.transform.rotation * GetAxis(PivotAxis);
        }
    }
}